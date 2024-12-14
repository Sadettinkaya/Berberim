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

        public LoginController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
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
				UserName = e.userName
			};

			if (e.sifre == e.sifreTekrar)
			{
				var result = await _userManager.CreateAsync(appUser, e.sifre); // Şifre doğruysa kullanıcı oluşturuluyor

				if (result.Succeeded)
				{
					// Kullanıcı oluşturuldu, şimdi User rolünü ata
					var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
					if (roleResult.Succeeded)
					{
						// Rol atanması da başarılıysa ana sayfaya yönlendir
						return RedirectToAction("Index", "Home");
					}
					else
					{
						// Rol atanması sırasında hata oluştuysa hata mesajlarını göster
						foreach (var error in roleResult.Errors)
						{
							ModelState.AddModelError("", error.Description);
						}
					}
				}
				else
				{
					// Kullanıcı oluşturma sırasında hata oluştuysa hata mesajlarını göster
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Şifreler birbiriyle eşleşmiyor.");
			}

			return View(e);
		}


	}
}
