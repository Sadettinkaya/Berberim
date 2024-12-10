using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Berberim.Controllers
{
    public class GirisController : Controller
    {
        private readonly SignInManager<AppUser> _singInManager; //oturum açmak
        private readonly UserManager<AppUser> _userManager;

        public GirisController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(GirisYapModel k)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(k.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Bu e-posta ile kayıtlı bir kullanıcı bulunamadı.");
                    return View(k);
                }

                var result = await _singInManager.PasswordSignInAsync(user, k.sifre, false, false);

                if (result.Succeeded)
                {
                    // Kullanıcı giriş işlemi başarılı
                    return RedirectToAction("KayitOl", "Login");
                }
                else
                {
                    // Hatalı giriş
                    ModelState.AddModelError("", "E-posta veya şifre hatalı. Lütfen bilgilerinizi kontrol edin.");
                    return View(k);
                }
            }

            // Model doğrulaması geçerli değilse
            return View(k);

        }
    }
}

