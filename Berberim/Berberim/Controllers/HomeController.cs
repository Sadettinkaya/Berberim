﻿using Berberim.Models;
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

            // OpenAI API'ye istek yap
            var result = await CallOpenAIAPI(filePath);
            ViewBag.Response = result;

            return View();
        }

        private async Task<string> CallOpenAIAPI(string filePath)
        {
            var apiUrl = "https://api.openai.com/v1/images/generations"; // DALL·E endpointi
            var apiKey = "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"; // OpenAI API Key

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // OpenAI API'ye JSON isteği hazırla
            var requestData = new
            {
                prompt = "Bu fotoğraftaki kişinin yüzünü aynı tutarak erkek saç modelini kısa ve modern bir şekilde değiştir.",
                n = 1, // Tek bir görsel üret
                size = "1024x1024"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI API Hatası: {responseContent}");
            }

            return responseContent;
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