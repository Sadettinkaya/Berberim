using Microsoft.AspNetCore.Mvc;

namespace Berberim.Areas.Admin.Controllers
{
    public class PersonelController : Controller
    {
        public IActionResult Listele()
        {
            return View();
        }
    }
}
