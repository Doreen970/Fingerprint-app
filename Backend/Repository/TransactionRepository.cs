using Backend.Data;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AuthorizeTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByClientIdAsync(int clientId)
        {
            return await _context.Transactions
                                 .Where(t => t.ClientId == clientId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByStaffIdAsync(string staffId)
        {
            return await _context.Transactions
                                 .Where(t => t.StaffId == staffId)
                                 .ToListAsync();
        }
    }
}
