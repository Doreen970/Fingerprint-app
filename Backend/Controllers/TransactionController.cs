using Backend.Dtos;
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
        private readonly IAccountMaskingService _accountMaskingService;

        public TransactionController(ITransactionRepository transactionRepository, IAccountMaskingService accountMaskingService)
        {
            _transactionRepository = transactionRepository;
            _accountMaskingService = accountMaskingService;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeTransaction(Transaction transaction)
        {
            try
            {
                var authorizedTransaction = await _transactionRepository.AuthorizeTransactionAsync(transaction);
                if (authorizedTransaction == null)
                    return NotFound("Client not found or transaction authorization failed");

                // Retrieve the client
                var client = await _transactionRepository.GetClientByIdAsync(transaction.ClientId);

                // Retrieve the staff member who served the client
                var staff = await _transactionRepository.GetStaffByIdAsync(transaction.StaffId);

                // Mask bank account number
        string maskedAccountNumber = _accountMaskingService.MaskBankAccountNumber(client.BankAccountNumber);

        // Construct the receipt
        var receipt = new Receipt
        {
            TransactionId = authorizedTransaction.Id,
            ClientName = $"{client.FirstName} {client.LastName}",
            BankAccountNumber = maskedAccountNumber,
            ServedBy = $"{staff.FirstName} {staff.LastName}",
            ServiceName = authorizedTransaction.Service.ServiceName,
            Amount = authorizedTransaction.Amount,
            Timestamp = authorizedTransaction.Timestamp
        };

        return Ok(receipt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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

        [HttpGet("all")]
        public async Task<ActionResult<List<Services>>> GetAllServices()
        {
            var services = await _transactionRepository.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessService([FromBody] ServiceRequestDto request)
        {
            var result = await _transactionRepository.ProcessServiceAsync(request.ClientId, request.StaffId, request.ServiceId, request.Amount);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
