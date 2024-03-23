using Microsoft.AspNetCore.Mvc;
using service;
using Microsoft.AspNetCore.OData.Query;
using BusinessObject.Model;

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
        public async Task<IActionResult> PlaceBid([FromBody] CreateBidRequestModel model)
        {
            if (model.Amount <= 0 || model.PropertyId <= 0 || model.UserId <= 0)
            {
                return BadRequest(new ResponseModel
                {
                    Status = "Error",
                    Message = "Invalid auction information. Please check again."
                });
            }
            bool bidSuccess = await _bidService.PlaceBidAsync(model.UserId, model.PropertyId, model.Amount);
            if (!bidSuccess)
            {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel
                    {
                        Status = "Error",
                        Message = "Bid creation failed! Bid amount must be greater than the current highest bid."
                    }
                );
            }
            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Bid created successfully!"
            });
        }

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