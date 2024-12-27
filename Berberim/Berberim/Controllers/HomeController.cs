using Berberim.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Berberim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private const string ApiKey = "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"; // API anahtarınızı buraya ekleyin
        private const string ApiEndpoint = "https://api.groq.com/openai/v1/chat/completions"; // Groq Vision API Endpoint

        public HomeController(IWebHostEnvironment environment, ILogger<HomeController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public IActionResult YapayZekaOneri()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> YapayZekaOneri(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewBag.Error = "Lütfen bir fotoğraf yükleyin.";
                return View();
            }

            // Fotoğrafı "uploads" klasörüne kaydet
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var filePath = Path.Combine(uploadsPath, photo.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Fotoğrafı Base64 formatına çevirin
            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                await photo.CopyToAsync(memoryStream);
                byte[] byteArray = memoryStream.ToArray();
                base64Image = Convert.ToBase64String(byteArray);
            }

            // JSON yükü oluştur
            var messagePayload = new
            {
                model = "llama-3.2-11b-vision-preview",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = "What hairstyle would you recommend for this photo?" },
                            new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                        }
                    }
                },
                temperature = 1,
                max_tokens = 1024,
                top_p = 1,
                stream = false
            };

            // API'ye istek gönder
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

                    var jsonPayload = JsonConvert.SerializeObject(messagePayload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ApiEndpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JObject.Parse(responseContent);
                        var message = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

                        // Yanıtı ViewBag'e ata
                        ViewBag.EditedImageUrl = message;
                        return View();
                    }

                    ViewBag.EditedImageUrl = "API isteği başarısız";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"API Hatası: {ex.Message}";
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

        public IActionResult İletisim()
        {
            return View();
        }

        public IActionResult Hizmetler()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
