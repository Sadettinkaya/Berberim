using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berberim.Controllers
{
    public class LoginController : Controller

    {
        //identitnin içinde bulunan kullanıcı kayit ve giriş islemleri için kullanılan classlardır
        private readonly UserManager<AppUser> _userManager; //kullanıcıların yönetimi
        private readonly SignInManager<AppUser> _singInManager; //oturum açmak

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }

       

        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(KullaniciKayitModel e)
        {
            AppUser appUser = new AppUser()
            {
                adSoyad = e.adSoyad,
                Email = e.Email,
                UserName = e.userName,

            };
            if (e.sifre == e.sifreTekrar)
            {
                var result = await _userManager.CreateAsync(appUser, e.sifre); //şifreler doğruysa kullanıcı oluşturuluyor

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(e);
        }

    }
}
