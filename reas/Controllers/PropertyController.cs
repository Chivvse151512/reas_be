using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace reas.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        [HttpGet()]
        public IEnumerable<Property> Get()
        {
            return null;
        }

        [HttpPost()]
        public IEnumerable<Property> create()
        {
            return null;
        }

        [HttpPost("update-property")]
        public IEnumerable<Property> update()
        {
            return null;
        }

        [HttpPost("update-status")]
        public IEnumerable<Property> updateStatus()
        {
            return null;
        }

        [HttpPost("update-price")]
        public IEnumerable<Property> updateCurrentPrice()
        {
            return null;
        }



    }
}
