using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("cash-deposit")]
        public async Task<IActionResult> CashDeposit(int clientId, string staffId, decimal amount, [FromBody] byte[] fingerprint)
        {
            var result = await _transactionRepository.ProcessServiceAsync(clientId, staffId, serviceId: 1, amount: amount, fingerprint: fingerprint); 
            if (!result.Success) return BadRequest("Transaction failed.");

            var receipt = await _transactionRepository.GenerateReceipt(result.Transaction.Id);
            if (receipt == null) return BadRequest("Failed to generate receipt.");

            return Ok(receipt);
        }

        [HttpPost("cash-withdrawal")]
        public async Task<IActionResult> CashWithdrawal(int clientId, string staffId, decimal amount, [FromBody] byte[] fingerprint)
        {
            var result = await _transactionRepository.ProcessServiceAsync(clientId, staffId, serviceId: 2, amount: amount, fingerprint: fingerprint); 
            if (!result.Success) return BadRequest("Transaction failed.");

            var receipt = await _transactionRepository.GenerateReceipt(result.Transaction.Id);
            if (receipt == null) return BadRequest("Failed to generate receipt.");

            return Ok(receipt);
        }

        [HttpPost("loan-disbursement")]
        public async Task<IActionResult> LoanDisbursement(int clientId, string staffId, decimal amount, [FromBody] byte[] fingerprint)
        {
            var result = await _transactionRepository.ProcessServiceAsync(clientId, staffId, serviceId: 3, amount: amount, fingerprint: fingerprint); 
            if (!result.Success) return BadRequest("Transaction failed.");

            var receipt = await _transactionRepository.GenerateReceipt(result.Transaction.Id);
            if (receipt == null) return BadRequest("Failed to generate receipt.");

            return Ok(receipt);
        }

        [HttpPost("edit-customer")]
        public async Task<IActionResult> EditCustomer(int clientId, string staffId, [FromBody] EditCustomerDto editCustomerDto)
        {
            try
            {
                var result = await _transactionRepository.ProcessServiceAsync(
                    clientId,
                    staffId,
                    serviceId: 4,
                    amount: 0,
                    fingerprint: null, // No fingerprint needed for basic details update
                    editCustomerDto: editCustomerDto
                );

                if (!result.Success)
                {
                    return BadRequest("Customer edit failed.");
                }

                return Ok(new { Message = "Customer edit successful.", Customer = editCustomerDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("currency-exchange")]
        public async Task<IActionResult> CurrencyExchange(int clientId, string staffId, decimal amount, string currencyCode, [FromBody] byte[] fingerprint)
        {
            var result = await _transactionRepository.ProcessServiceAsync(clientId, staffId, serviceId: 5, amount: amount, fingerprint: fingerprint, currencyCode: currencyCode); 

            if (!result.Success) return BadRequest("Currency exchange failed.");

            return Ok(new { Message = "Currency exchange successful.", Amount = amount, CurrencyCode = currencyCode });
        }


        [HttpPost("delete-customer")]
        public async Task<IActionResult> DeleteCustomer(int clientId, string staffId, [FromBody] byte[] fingerprint)
        {
            var result = await _transactionRepository.ProcessServiceAsync(clientId, staffId, serviceId: 6, amount: 0, fingerprint: fingerprint); 

            if (!result.Success) return BadRequest("Customer deletion failed.");

            return Ok(new { Message = "Customer deletion successful." });
        }


        [HttpGet("transactions-by-client")]
        public async Task<IActionResult> GetTransactionsByClientId(int clientId)
        {
            var transactions = await _transactionRepository.GetTransactionsByClientIdAsync(clientId);
            return Ok(transactions);
        }

        [HttpGet("transactions-by-staff")]
        public async Task<IActionResult> GetTransactionsByStaffId(string staffId)
        {
            var transactions = await _transactionRepository.GetTransactionsByStaffIdAsync(staffId);
            return Ok(transactions);
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _transactionRepository.GetAllServicesAsync();
            return Ok(services);
        }
    }
}
