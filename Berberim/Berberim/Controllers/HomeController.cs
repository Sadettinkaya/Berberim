using Berberim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace Berberim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _environment;

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

            // Fotoğrafı kaydet
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", photo.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Hugging Face API'ye istek yap
            var result = await CallHuggingFaceAPI(filePath);
            ViewBag.Response = result;

            return View();
        }

        private async Task<string> CallHuggingFaceAPI(string filePath)
        {
            var apiUrl = "https://api-inference.huggingface.co/models/facebook/deit-small-patch16-224"; //model
            var apiKey = "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"; // Hugging Face API Key

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            using var form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(filePath)), "file", Path.GetFileName(filePath));

            var response = await client.PostAsync(apiUrl, form);
            return await response.Content.ReadAsStringAsync();
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