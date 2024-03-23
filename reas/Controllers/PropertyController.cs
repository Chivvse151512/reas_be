using System.Security.Claims;
using BusinessObject;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using reas.Model;
using service;

namespace reas.Controllers
{
    [Route("api/[controller]")]
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
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                propertyService.create(int.Parse(userId), request);
                return Ok(new ResponseModel { Status = "Success", Message = "Property created successfully" });


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                // Lấy userId từ claim trong token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                // Chuyển đổi userId từ string sang int
                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    return BadRequest("Invalid user ID.");
                }
                propertyService.updateStatus(request, userId);
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
        [Authorize]
        public IActionResult UpdateCurrentPrice(UpdatePricePropertyRequest request)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (role == null || role != "3")
                {
                    throw new Exception("Only user can be update price!");
                }


                propertyService.updatePrice(int.Parse(userId), request);
                return Ok(new ResponseModel { Status = "Success", Message = "Property price updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("to-verify")]
        [EnableQuery]
        public IActionResult GetPropertiesToVerify()
        {
            try
            {
                var staffId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (staffId == null || role == null)
                {
                    return Unauthorized();
                }
                if (role != "STAFF")
                {
                    return Forbid(); 
                }
                var properties = propertyService.GetPropertiesToVerify(int.Parse(staffId));
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("finished")]
        [EnableQuery]
        public IActionResult GetFinishedPropertiesByUser()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (userId == null || role == null)
                {
                    return Unauthorized();
                }
                if (role != "CUSTOMER")
                {
                    return Forbid();
                }
                var properties = propertyService.GetFinishedPropertiesByUser(int.Parse(userId));
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("by-user")]
        [EnableQuery]
        public IActionResult GetPropertiesByUser()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if (userId == null || role == null)
                {
                    return Unauthorized();
                }
                if (role != "CUSTOMER")
                {
                    return Forbid();
                }
                var properties = propertyService.GetPropertiesByUser(int.Parse(userId));
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
                var properties = propertyService.GetPropertiesByStatus(statusId);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }
    }
}
