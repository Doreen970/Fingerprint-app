using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Client> GetClientByIdAsync(int clientId);
        Task<Staff> GetStaffByIdAsync(string staffId);
        Task<Transaction> AuthorizeTransactionAsync(Transaction transaction, byte[] fingerprint);
        Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId);
        Task<IEnumerable<Transaction>> GetTransactionsByStaffIdAsync(string staffId);
        Task<List<Services>> GetAllServicesAsync();
        //Task<bool> ProcessServiceAsync(int clientId, string staffId, int serviceId, decimal amount, byte[] fingerprint, EditCustomerDto editCustomerDto = null, string currencyCode = null);
        Task<(bool Success, Transaction Transaction, Client Client, Staff Staff)> ProcessServiceAsync(
    int clientId, string staffId, int serviceId, decimal amount, byte[] fingerprint, EditCustomerDto editCustomerDto = null, string currencyCode = null);
        Task<Receipt> GenerateReceipt(int transactionId);
    }
}
