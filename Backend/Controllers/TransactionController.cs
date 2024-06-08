using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeTransaction(Transaction transaction)
        {
            var result = await _transactionRepository.AuthorizeTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionsByClientId), new { clientId = transaction.ClientId }, result);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetTransactionsByClientId(int clientId)
        {
            var transactions = await _transactionRepository.GetTransactionsByClientIdAsync(clientId);
            return Ok(transactions);
        }

        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetTransactionsByStaffId(string staffId)
        {
            var transactions = await _transactionRepository.GetTransactionsByStaffIdAsync(staffId);
            return Ok(transactions);
        }
    }
}
