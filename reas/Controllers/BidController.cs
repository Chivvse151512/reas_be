using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using service;
using reas.Model;
using Microsoft.AspNetCore.OData.Query;

namespace reas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        // POST api/bid
        [HttpPost]
        public IActionResult CreateBid([FromBody] CreateBidRequestModel model)
        {
            if (model.Amount < 0 || model.PropertyId <= 0 || model.UserId <= 0)
            {
                return BadRequest(new ResponseModel
                {
                    Status = "Error",
                    Message = "Invalid auction information. Please check again."
                });
            }
            var bid = new Bid
            {
                UserId = model.UserId,
                PropertyId = model.PropertyId,
                Amount = model.Amount,
                Status = 1, // Active status
                CreatedAt = DateTime.UtcNow
            };
            var createdBid = _bidService.Create(bid);
            if (createdBid == null || createdBid.Id <= 0)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel
                    {
                        Status = "Error",
                        Message = "Bid creation failed! Please check bid details and try again."
                    }
                );
            }
            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Bid created successfully!"
            });
        }

        // GET api/bid/property/{id}
        [EnableQuery]
        [HttpGet("property/{id}")]
        public ActionResult GetBidsByPropertyId(int id, int pageNumber, int pageSize)
        {
            try
            {
                var bids = _bidService.GetListByPropertyId(id, pageNumber, pageSize);
                if (bids == null || !bids.Any())
                {
                    return NotFound(new ResponseModel
                    {
                        Status = "Error",
                        Message = "No bids found for the specified property."
                    });
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "Bids retrieved successfully.",
                    Data = bids
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    Status = "Error",
                    Message = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }
    }
}