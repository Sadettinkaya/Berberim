
using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Berberim.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RandevuController : Controller
    {

        private readonly Appdb_context _context;

        public RandevuController(Appdb_context context)
        {
            _context = context;
        }

        public IActionResult RandevuListesi()
        {
            var randevular = _context.randevus
                                       .Include(p => p.personel).ToList();

            return View(randevular);
        }


        [HttpGet]
        public IActionResult DeleteAppoint(int id)
        {

            var personel = _context.randevus.Find(id);

            if (personel != null)
            {
                _context.randevus.Remove(personel); // Sil
                _context.SaveChanges(); // Veritabanında değişiklikleri kaydet
                return RedirectToAction("RandevuListesi");
            }

            return RedirectToAction("RandevuListesi");
        }


        [HttpPost]
        public async Task<IActionResult> Onayla(int id)
        {
            var randevu = await _context.randevus.FindAsync(id);

            if (randevu == null)
            {
                return NotFound();
            }

            randevu.onaylandimi = true;

            await _context.SaveChangesAsync();
            //  return Content("<script>window.location.reload();</script>"); // Sayfayı yeniler

            return RedirectToAction("RandevuListesi");
        }



        [HttpGet]
        public async Task<IActionResult> UpdateAppoint(int id)
        {

            var appointment = await _context.randevus

                                     .Include(a => a.personel) // Personel bilgilerini de dahil et
                                     .FirstOrDefaultAsync(p => p.randevuID == id);
            if (appointment == null)
            {
                return RedirectToAction("RandevuListesi"); // Personel bulunamazsa listeye yönlendir
            }

            var services = _context.hizmets?.ToList() ?? new List<Hizmet>();

            ViewBag.Services = services;
            ViewBag.PersonnelList = _context.personnels.ToList();

            return View(appointment); // Bulunan personel bilgilerini View'e gönder
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAppoint(Randevu p)
        {


            if (ModelState.IsValid) // Form verileri geçerli mi?
            {
                var randevu = await _context.randevus.FirstOrDefaultAsync(per => per.randevuID == p.randevuID); // Eski kaydı bul



                if (randevu != null)
                {
                    // Alanları güncelle

                    randevu.MusteriName = p.MusteriName;
                    randevu.MusteriID = p.MusteriID;
                    randevu.personelID = p.personelID;
                    randevu.hizmetID = p.hizmetID;
                    randevu.onaylandimi = p.onaylandimi;
                    randevu.notes = p.notes;
                    randevu.tel = p.tel;
                    randevu.MusteriName = p.MusteriName;
                    randevu.hizmetName = p.hizmetName;
                    randevu.randevuTarih = p.randevuTarih;
                    randevu.randevuSaat = p.randevuSaat;



                    _context.randevus.Update(randevu);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("RandevuListesi"); // Başarılı olursa listeye dön
                }
            }

            var services = await _context.hizmets.ToListAsync();
            ViewBag.Services = services;
            ViewBag.PersonnelList = _context.personnels.ToList();

            return View(p); // Hatalıysa aynı sayfayı yeniden yükle
        }







    }


}