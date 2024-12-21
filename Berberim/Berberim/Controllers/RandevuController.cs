using Berberim.Db_context;
using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Berberim.Controllers
{
	public class RandevuController : Controller
	{
		private readonly Appdb_context _context;



		public RandevuController(Appdb_context context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult YeniRandevu()
		{
			// Veritabanından hizmetleri çek
			var services = _context.hizmets?.ToList() ?? new List<Hizmet>();
			var personels = _context.personnels?.ToList() ?? new List<Personel>();


			// Hizmetleri View'a gönder
			ViewBag.Services = services;
			ViewBag.Personels = personels;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> YeniRandevu(RandevuModel model)
		{

			if (ModelState.IsValid)
			{
				// serviceID'ye karşılık gelen hizmeti al
				//  var service = await _context.services.FindAsync(model.service);

				var service = await _context.hizmets.FirstOrDefaultAsync(s => s.hizmetID == int.Parse(model.service));
				var personel = await _context.personnels.FirstOrDefaultAsync(s => s.personelID == int.Parse(model.personnel));

				if (service == null || personel == null)
				{
					//  ModelState.AddModelError("service", "Geçersiz hizmet seçimi.");
					return View(model);
				}

                // Seçilen personel ve tarih bilgilerini al

                //   var selectedDate = model.date; // model.date'in sadece tarih kısmı
                //  selectedDate = selectedDate.ToUniversalTime();
                // var selectedDate = model.date.ToUniversalTime();
                var selectedDate = DateTime.SpecifyKind(model.date, DateTimeKind.Local).ToUniversalTime();


                var appointHour = model.saat;
                var selectedPersonnelId = int.Parse(model.personnel);




                // Mevcut randevuları kontrol et
                var existingAppointments = await _context.randevus
                    .Where(a => a.personelID == selectedPersonnelId &&
                                a.randevuTarih.Date == selectedDate && // Aynı tarihteki randevular
                                a.randevuSaat == appointHour) // Aynı saatteki randevular
                    .ToListAsync();


                if (existingAppointments.Any())
                {
                    // Çakışma var, kullanıcıya hata mesajı göster
                    ModelState.AddModelError(string.Empty, "Seçtiğiniz saatte bu personel için bir randevu bulunmaktadır. Lütfen başka bir saat seçiniz.");
                    // Eğer model geçerli değilse tekrar formu ve hizmetleri yükle
                    ViewBag.Services = _context.hizmets.ToList();
                    ViewBag.Personels = _context.personnels.ToList();
                    return View(model);
                }






                // Veritabanına kaydedilecek Appointment entity'sini oluştur
                var _randevuTarih = DateTime.SpecifyKind(model.date, DateTimeKind.Local).ToUniversalTime();

				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Kullanıcı kimliğini al


				var randevu = new Randevu
				{
					MusteriName = model.adSoyad,
					hizmetID = int.Parse(model.service), // service string olduğu için dönüştürülmeli
					hizmetName = service.hizmetName, // Burada dolduruluyor
					randevuTarih = _randevuTarih,
					randevuSaat = model.saat,
					tel = model.PhoneNumber,
					notes = model.Notes,

					onaylandimi = false,
					personelID = int.Parse(model.personnel),
					MusteriID = int.Parse(userId),

				};

				await _context.randevus.AddAsync(randevu);
				await _context.SaveChangesAsync();

                await Updatekazancs(int.Parse(model.personnel), _randevuTarih, service.hizmetPrice);


                // Başarılı işlem sonrası yönlendirme
                return RedirectToAction("Index", "Home");
			}
			else
			{
				// Eğer model geçerli değilse tekrar formu ve hizmetleri yükle
				ViewBag.Services = _context.hizmets.ToList();
				ViewBag.Personels = _context.personnels.ToList();
				return View(model);
			}

		}



        private async Task Updatekazancs(int personnelID, DateTime date, decimal servicePrice)
        {
            // Sadece gün kısmını kullan
            var targetDate = date.Date;

            // Aynı personelin aynı gününe ait kazançları bul
            var existingEarnings = await _context.kazancs
                .FirstOrDefaultAsync(e => e.PersonnelID == personnelID && e.Date == targetDate);

            if (existingEarnings != null)
            {
                // Mevcut kayıt varsa toplam kazanca ekle
                existingEarnings.TotalPersonelKazanc += servicePrice;
                _context.kazancs.Update(existingEarnings);
            }
            else
            {
                // Yeni bir kazanç kaydı oluştur
                var newEarnings = new PersonelKazanc
                {
                    PersonnelID = personnelID,
                    Date = targetDate, // Saf tarih bilgisi kaydediliyor
                    TotalPersonelKazanc = servicePrice
                };
                await _context.kazancs.AddAsync(newEarnings);
            }

            await _context.SaveChangesAsync();
        }






    }
}