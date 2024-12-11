using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Controllers
{
	public class AdminLayoutController : Controller
	{

		private readonly Appdb_context _context;

		public AdminLayoutController(Appdb_context context)
		{
			_context = context;
		}

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
        public async Task< IActionResult> EklePer(Personel per)
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


        //public IActionResult GuncellePer()
        //{
        //	return View();
        //}


    }
}
