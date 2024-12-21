using Berberim.Db_context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KazancController : Controller
    {
        private readonly Appdb_context _context;

        public KazancController(Appdb_context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.kazancs
                               .Include(p => p.Personnel) // Navigasyon özelliğini dahil et
                               .ToList();
            return View(model);
        }






    }
}