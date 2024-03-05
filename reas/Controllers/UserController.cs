using Microsoft.AspNetCore.Mvc;

namespace reas.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
