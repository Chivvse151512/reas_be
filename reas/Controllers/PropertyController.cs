using BusinessObject;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using reas.Model;
using service;

namespace reas.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyService propertyService;
        private readonly ILogger<PropertyController> logger;

        public PropertyController(IPropertyService propertyService, ILogger<PropertyController> logger)
        {
            this.propertyService = propertyService;
            this.logger = logger;
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
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("create")]
        public IActionResult create([FromBody] CreatePropertyRequest request)
        {
            try
            {
                propertyService.create(request);
                return Ok(new ResponseModel { Status = "Success", Message = "Property created successfully" });


            } catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("update-property")]
        public IActionResult update()
        {
            return null;
        }

        [HttpPost("update-status")]
        public IActionResult UpdateStatus([FromBody] UpdateStatusPropertyRequest request)
        {
            try
            {
                propertyService.updateStatus(request);
                return Ok(new ResponseModel { Status = "Success", Message = "Property status updated successfully" });
            }
            catch (ArgumentException ex)
            {
                logger.LogWarning(ex, "Invalid argument provided to UpdateStatus.");
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                logger.LogWarning(ex, "Invalid operation attempted in UpdateStatus.");
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred while updating the property status.");
                return StatusCode(500, new ResponseModel { Status = "Error", Message = "An error occurred while updating the property status." });
            }
        }

        [HttpPost("update-price")]
        public IActionResult UpdateCurrentPrice(UpdatePricePropertyRequest request)
        {
            try
            {
                propertyService.updatePrice(request);
                return Ok(new ResponseModel { Status = "Success", Message = "Property price updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("to-verify/{staffId}")]
        [EnableQuery]
        public IActionResult GetPropertiesToVerify(int staffId)
        {
            try
            {
                var properties = propertyService.GetPropertiesToVerify(staffId);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("finished/{userId}")]
        [EnableQuery]
        public IActionResult GetFinishedPropertiesByUser(int userId)
        {
            try
            {
                var properties = propertyService.GetFinishedPropertiesByUser(userId);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        [EnableQuery]
        public IActionResult GetPropertiesByUser(int userId)
        {
            try
            {
                var properties = propertyService.GetPropertiesByUser(userId);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{propertyId}")]
        public IActionResult GetPropertiesWithBids(int propertyId)
        {
            try
            {
                var propertyWithBids = propertyService.GetPropertyWithBids(propertyId).ToList();
                if (propertyWithBids != null)
                {
                    return Ok(propertyWithBids);
                }
                else
                {
                    return NotFound(new ResponseModel { Status = "Error", Message = "Property not found" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the property with bids.");
                return StatusCode(500, new ResponseModel { Status = "Error", Message = "An error occurred while retrieving the property with bids." });
            }
        }

        [HttpGet("by-status")]
        [EnableQuery]
        public IActionResult GetPropertiesByStatus(int statusId)
        {
            try
            {
                var properties = propertyService.GetPropertiesByStatus(statusId).ToList();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }
    }
}
