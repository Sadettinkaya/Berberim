using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Berberim.Controllers
{
    public class PhotoRecommendationController : Controller
    {
        private readonly Cloudinary _cloudinary;
        private readonly HttpClient _httpClient;

        public PhotoRecommendationController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewBag.Error = "Lütfen bir fotoğraf seçin.";
                return View();
            }

            // 1. **Cloudinary'ye Fotoğraf Yükle**
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(photo.FileName, photo.OpenReadStream()),
                Folder = "uploaded_photos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ViewBag.Error = "Fotoğraf yüklenirken bir hata oluştu.";
                return View();
            }

            // 2. **Cloudinary'den URL Al**
            string imageUrl = uploadResult.SecureUrl.ToString();

            // 3. **Replicate API için JSON İsteği**
            var requestBody = new
            {
                input = new
                {
                    image = imageUrl,
                    prompt = "Bir saç modeli önerisi oluşturun." // Buraya prompt ekleniyor
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Replicate API'ye istek gönder
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Token r8_M7tnwRvxz4Arp44cG1633Oe9kkFWvFI2fYW6Y");
            var response = await _httpClient.PostAsync("https://api.replicate.com/v1/models/black-forest-labs/flux-dev/predictions", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"API Hatası: {errorResponse}";
                return View();
            }

            // 4. **Yanıtı İşle**
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

            var predictionId = jsonResponse.GetProperty("id").GetString();
            bool isComplete = false;
            int attempts = 0;

            while (!isComplete && attempts < 10)
            {
                await Task.Delay(5000); // 5 saniye bekle
                var getResponse = await _httpClient.GetAsync($"https://api.replicate.com/v1/predictions/{predictionId}");
                var getJsonResponse = await getResponse.Content.ReadAsStringAsync();
                var getStatusJson = JsonSerializer.Deserialize<JsonElement>(getJsonResponse);

                var status = getStatusJson.GetProperty("status").GetString();
                if (status == "succeeded")
                {
                    isComplete = true;

                    if (getStatusJson.TryGetProperty("output", out var output) && output.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var result in output.EnumerateArray())
                        {
                            ViewBag.ImageResult = result.GetString(); // API'den dönen öneri görsel URL'si
                        }
                    }
                }
                else if (status == "failed")
                {
                    ViewBag.Error = "API isteği başarısız oldu.";
                    return View();
                }

                attempts++;
            }

            return View();
        }
    }
}
