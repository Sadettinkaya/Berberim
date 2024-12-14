using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berberim.Controllers
{
	public class GirisController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager; // Oturum açmak
		private readonly UserManager<AppUser> _userManager;    // Kullanıcı yönetimi

		public GirisController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// Giriş Sayfası
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> AssignRoleToUser(string email, string roleName)
		{
			// Kullanıcıyı e-posta ile bul
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return Ok("Kullanıcı bulunamadı: {email}");
			}

			// Eğer kullanıcı zaten role sahipse
			if (await _userManager.IsInRoleAsync(user, roleName))
			{
				return Ok($"Kullanıcı zaten {roleName} rolüne sahip.");
			}

			// Rolü kullanıcıya ata
			var result = await _userManager.AddToRoleAsync(user, roleName);
			if (result.Succeeded)
			{
				return Ok($"{roleName} rolü {email} kullanıcısına başarıyla atandı.");
			}

			// Hata durumunda
			return BadRequest("Rol atama işlemi başarısız.");
		}



		// Giriş İşlemi
		[HttpPost]
		public async Task<IActionResult> Index(GirisYapModel k)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Lütfen gerekli tüm alanları doldurun.");
				return View(k);
			}

			// Kullanıcıyı e-posta ile bul
			var user = await _userManager.FindByEmailAsync(k.Email);
			if (user == null)
			{
				ModelState.AddModelError("", "Bu e-posta ile kayıtlı bir kullanıcı bulunamadı.");
				return View(k);
			}

			// Şifre doğrula ve oturum aç
			var result = await _signInManager.PasswordSignInAsync(user, k.sifre, false, false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "E-posta veya şifre hatalı. Lütfen bilgilerinizi kontrol edin.");
				return View(k);
			}

			// Rol kontrolü yap ve kullanıcıyı yönlendir
			var roles = await _userManager.GetRolesAsync(user);

			if (roles.Contains("Admin"))
			{
				// Admin kullanıcı admin paneline yönlendirilir
				return RedirectToAction("ListelePer", "AdminLayout", new { area = "Admin" });
			}
			else if (roles.Contains("User"))
			{
				// User kullanıcı ana sayfaya yönlendirilir
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("", "Kullanıcının atanmış bir rolü bulunamadı.");
				return View(k);
			}
		}

		// Çıkış Yapma
		[HttpPost]
		public async Task<IActionResult> CikisYap()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
