using System;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using service;

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
        public ActionResult<Bid> CreateBid([FromBody] Bid bid)
        {
            try
            {
                var createdBid = _bidService.Create(bid);
                if (createdBid == null)
                {
                    return StatusCode(500, "An error occurred while creating the bid.");
                }
                return Ok(createdBid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // GET api/bid/property/{id}
        [HttpGet("property/{id}")]
        public ActionResult<List<Bid>> GetBidsByPropertyId(int id)
        {
            try
            {
                var bids = _bidService.GetListByPropertyId(id);
                if (bids == null)
                {
                    return StatusCode(500, "An error occurred while retrieving bids for the property.");
                }
                return Ok(bids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}