using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using BusinessObject.Model;
using service;

namespace reas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }

        // GET api/deposit
        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IList<Deposit>> GetAllDeposits()
        {
            var deposits = _depositService.GetListByStatusNotZero();
            if (deposits == null || !deposits.Any())
            {
                return NotFound(new ResponseModel
                {
                    Status = "Error",
                    Message = "No deposits found."
                });
            }
            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Deposits retrieved successfully.",
                Data = deposits
            });
        }

        // GET api/deposit/{id}
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Deposit> GetDepositById(int id)
        {
            var deposit = _depositService.GetById(id);
            if (deposit == null)
            {
                return NotFound(new ResponseModel
                {
                    Status = "Error",
                    Message = "Deposit not found."
                });
            }
            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Deposit retrieved successfully.",
                Data = deposit
            });
        }

        // POST api/deposit
        [Authorize]
        [HttpPost]
        public IActionResult CreateDeposit([FromBody] CreateDepositModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDeposit = _depositService.Insert(model.UserId, model.PropertyId, model.Amount);
            if (createdDeposit == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    Status = "Error",
                    Message = "An error occurred while creating deposit."
                });
            }
            return CreatedAtAction(nameof(GetDepositById), new { id = createdDeposit.Id }, new ResponseModel
            {
                Status = "Success",
                Message = "Deposit created successfully.",
                Data = createdDeposit
            });
        }

        // PUT api/deposit/{id}/updatestatus
        [Authorize]
        [HttpPut("{id}/updatestatus")]
        public IActionResult UpdateDepositStatus(int id, [FromBody] UpdateDepositStatusModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.NewStatus <= 0) 
            {
                return BadRequest("New status must be greater than 0.");
            }

            var updatedDeposit = _depositService.UpdateStatus(id, model.NewStatus);
            if (updatedDeposit == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    Status = "Error",
                    Message = $"An error occurred while updating deposit with ID: {id}."
                });
            }
            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Deposit status updated successfully.",
                Data = updatedDeposit
            });
        }

        // GET api/deposit/check
        [Authorize]
        [HttpGet("check")]
        public ActionResult CheckDeposit([FromQuery] CheckDepositModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var depositExists = _depositService.CheckDeposit(model.UserId, model.PropertyId);
                if (!depositExists.HasValue)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                    {
                        Status = "Error",
                        Message = "An error occurred while checking deposit existence."
                    });
                }
                return Ok(new ResponseModel
                {
                    Status = "Success",
                    Message = depositExists.Value ? "Deposit exists." : "Deposit does not exist."
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