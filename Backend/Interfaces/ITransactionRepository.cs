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
        Task<Transaction> AuthorizeTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId);
        Task<IEnumerable<Transaction>> GetTransactionsByStaffIdAsync(string staffId);
        Task<List<Services>> GetAllServicesAsync();
        Task<bool> ProcessServiceAsync(int clientId, string staffId, int serviceId, decimal amount, EditCustomerDto editCustomerDto = null, string currencyCode = null);
    }
}
