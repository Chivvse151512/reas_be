using BusinessObject;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using service;

namespace reas.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private IPropertyService propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }


        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                List<Property> properties =  propertyService.get().ToList();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(Message.of(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult create([FromBody] CreatePropertyRequest request)
        {
            try
            {
                propertyService.create(request);
                return Ok();


            } catch (Exception ex)
            {
                return BadRequest(Message.of(ex.Message));
            }
        }

        [HttpPost("update-property")]
        public IActionResult update()
        {
            return null;
        }

        [HttpPost("update-status")]
        public IActionResult updateStatus(UpdateStatusPropertyRequest request)
        {
            try
            {
                propertyService.updateStatus(request);
                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(Message.of(ex.Message));
            }
        }

        [HttpPost("update-price")]
        public IActionResult updateCurrentPrice(UpdatePricePropertyRequest request)
        {
            try
            {
                propertyService.updatePrice(request);
                return Ok();


            }
            catch (Exception ex)
            {
                return BadRequest(Message.of(ex.Message));
            }
        }



    }
}
