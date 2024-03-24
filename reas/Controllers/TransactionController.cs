using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using service;
using BusinessObject.Model;
using BusinessObject;
using Microsoft.AspNetCore.OData.Query;

namespace reas.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                transaction.UserId = userId;
                transaction.CreatedAt = DateTime.Now;
                transaction.Status = 1;

                await _transactionService.CreateTransactionAsync(transaction);
                return Ok(new ResponseModel { Status = "Success", Message = "Transaction created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTransaction()
        {
            var transactions = _transactionService.GetAllTransaction();
            if (transactions == null || !transactions.Any())
            {
                return NotFound(new ResponseModel { Status = "Error", Message = "No transactions found." });
            }
            return Ok(new ResponseModel { Status = "Success", Data = transactions });
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetTransactionsByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);
            var transactions = _transactionService.GetTransactionsByUserId(userId);
            if (transactions == null || !transactions.Any())
            {
                return NotFound(new ResponseModel { Status = "Error", Message = "No transactions found." });
            }
            return Ok(new ResponseModel { Status = "Success", Data = transactions });
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            try
            {
                var transaction = _transactionService.GetTransactionById(id);
                if (transaction == null)
                {
                    return NotFound(new ResponseModel { Status = "Error", Message = "Transaction not found." });
                }
                return Ok(new ResponseModel { Status = "Success", Data = transaction });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction([FromBody] Transaction transaction)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);
                var existingTransaction = GetTransactionById(transaction.Id);
                if (existingTransaction == null)
                {
                    return NotFound(new ResponseModel { Status = "Error", Message = "News not found." });
                }
                transaction.UserId = userId;
                transaction.UpdatedAt = DateTime.Now;
                await _transactionService.UpdateTransactionAsync(transaction);
                return Ok(new ResponseModel { Status = "Success", Message = "Transaction updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                return Ok(new ResponseModel { Status = "Success", Message = "Transaction deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }
    }
}

