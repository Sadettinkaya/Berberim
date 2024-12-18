using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminLayoutController : Controller
    {

        private readonly Appdb_context _context;

        public AdminLayoutController(Appdb_context context)
        {
            _context = context;
        }

       
        [HttpGet]
        public IActionResult ListelePer()
        {
            var model = _context.personnels
                        .Include(a => a.Uzmanliks).ToList(); // Navigasyon özelliğini dahil et


            return View(model);

        }

        public IActionResult EklePer()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> EklePer(Personel per)
        {
            if (ModelState.IsValid)
            {

                var salon = await _context.salons.FirstOrDefaultAsync(s => s.salonID == 1);

                if (salon == null)
                {
                    // Hata mesajı, salon bulunamadı
                    ModelState.AddModelError("salonID", "Geçerli bir salon seçmediniz.");
                    return View(per);
                }

                Personel personel = new Personel()
                {
                    personelName = per.personelName,
                    personelEmail = per.personelEmail,
                    personelPassword = per.personelPassword,
                    musaitSaat = per.musaitSaat,
                    UzmanlikID = per.UzmanlikID,
                    salonID = 1

                };

                _context.personnels.Add(personel); // Yeni personel ekle
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet

                return RedirectToAction("ListelePer", "AdminLayout");

            }


            return View(per);


        }

        public IActionResult SilPer(int id)
        {
            var personel = _context.personnels.Find(id);

            if (personel != null)
            {
                _context.personnels.Remove(personel);
                _context.SaveChanges();

                return RedirectToAction("ListelePer", "AdminLayout");
            }

            return View();
        }

        public async Task<IActionResult> GuncellePer(int id)
        {

            var personel = await _context.personnels.FirstOrDefaultAsync(p => p.personelID == id); // ID'ye göre personel bul

            if (personel == null)
            {
                return RedirectToAction("ListelePer", "AdminLayout"); // Personel bulunamazsa listeye yönlendir
            }
            return View(personel); // Bulunan personel bilgilerini View'e gönder
        }


        [HttpPost]
        public async Task<IActionResult> GuncellePer(Personel p)
        {
            if (ModelState.IsValid) // Form verileri geçerli mi?
            {
                var personel = await _context.personnels.FirstOrDefaultAsync(per => per.personelID == p.personelID); // Eski kaydı bul

                if (personel != null)
                {
                    // Alanları güncelle
                    personel.personelName = p.personelName;
                    personel.personelID = p.personelID;
                    personel.musaitSaat = p.musaitSaat;
                    personel.personelEmail = p.personelEmail;
                    personel.personelPassword = p.personelPassword;
                    personel.UzmanlikID = p.UzmanlikID;
                    personel.salonID = 1;

                    _context.personnels.Update(personel);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("ListelePer", "AdminLayout"); // Başarılı olursa listeye dön
                }
            }
            return View(p); // Hatalıysa aynı sayfayı yeniden yükle
        }

    }
}
