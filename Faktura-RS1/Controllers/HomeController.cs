using Microsoft.AspNetCore.Mvc;
using RS1_Faktura.EF;

namespace RS1_Faktura.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public IActionResult TestDB()
        {
            MojDBInitializer.Podaci();
            return View(new MojContext());
        }
    }
}