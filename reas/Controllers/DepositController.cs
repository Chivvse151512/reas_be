using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<List<Deposit>> GetAllDeposits()
        {
            var deposits = _depositService.GetListByStatusNotZero();
            if (deposits == null)
            {
                return StatusCode(500, "An error occurred while retrieving deposits.");
            }
            return Ok(deposits);
        }

        [HttpGet("{id}")]
        public ActionResult<Deposit> GetDepositById(int id)
        {
            var deposit = _depositService.GetById(id);
            if (deposit == null)
            {
                return NotFound("Deposit not found.");
            }
            return Ok(deposit);
        }


        [HttpPost]
        public ActionResult<Deposit> CreateDeposit(Deposit deposit)
        {
            var createdDeposit = _depositService.Insert(deposit.UserId, deposit.PropertyId, deposit.Amount);
            if (createdDeposit == null)
            {
                return StatusCode(500, "An error occurred while creating deposit.");
            }
            return CreatedAtAction(nameof(GetDepositById), new { id = createdDeposit.Id }, createdDeposit);
        }

        [HttpPut("{id}/updatestatus/{newStatus}")]
        public ActionResult<Deposit> UpdateDepositStatus(int id, int newStatus)
        {
            var updatedDeposit = _depositService.UpdateStatus(id, newStatus);
            if (updatedDeposit == null)
            {
                return StatusCode(500, $"An error occurred while updating deposit with ID: {id}.");
            }
            return Ok(updatedDeposit);
        }
    }
}

