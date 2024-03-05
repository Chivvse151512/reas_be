using Microsoft.AspNetCore.Mvc;

namespace reas.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
