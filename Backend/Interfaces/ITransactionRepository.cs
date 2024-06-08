using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> AuthorizeTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId);
        Task<IEnumerable<Transaction>> GetTransactionsByStaffIdAsync(string staffId);
    }
}
