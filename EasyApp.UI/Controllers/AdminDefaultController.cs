using Microsoft.AspNetCore.Mvc;

namespace EasyApp.UI.Controllers
{
    public class AdminDefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
