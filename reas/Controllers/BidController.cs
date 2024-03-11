using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace reas.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class BidController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

