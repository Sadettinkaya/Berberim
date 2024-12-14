using Microsoft.AspNetCore.Mvc;

namespace Berberim.Areas.Admin.Controllers
{
    public class PersonelController : Controller
    {
		[Area("Admin")]
		public IActionResult Listele()
        {
            return View();
        }
    }
}
