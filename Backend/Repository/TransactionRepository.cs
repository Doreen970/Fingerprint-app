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

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        public async Task<Staff> GetStaffByIdAsync(string staffId)
        {
            return await _context.Staffs.FindAsync(staffId);
        }

        //public async Task<Transaction> AuthorizeTransactionAsync(Transaction transaction)
        //{
        //    _context.Transactions.Add(transaction);
        //    await _context.SaveChangesAsync();
        //    return transaction;
        //}

        public async Task<Transaction> AuthorizeTransactionAsync(Transaction transaction)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientID == transaction.ClientId);
                if (client == null)
                    return null; 

                // Verify the fingerprint
                // implement the VerifyFingerprint method or provide the logic 

                // Finalize the transaction
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return transaction;
            }
            catch (Exception ex)
            {
                return null; 
            }
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

        public async Task<List<Services>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<bool> ProcessServiceAsync(int clientId, string staffId, int serviceId, decimal amount, EditCustomerDto editCustomerDto = null, string currencyCode = null)
        {
            var client = await _context.Clients.Include(c => c.Account).FirstOrDefaultAsync(c => c.ClientID == clientId);
            if (client == null) return false;

            var service = await _context.Services.FindAsync(serviceId);
            if (service == null) return false;

            var transaction = new Transaction
            {
                ClientId = clientId,
                StaffId = staffId,
                ServiceID = serviceId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);

            switch (service.ServiceCode)
            {
                case "CASH_DEP":
                    client.Account.Balance += amount;
                    break;
                case "CASH_WDL":
                    if (client.Account.Balance >= amount)
                    {
                        client.Account.Balance -= amount;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "LOAN_DISB":
                    client.Account.Balance += amount;
                    var loan = new Loan
                    {
                        ClientId = clientId,
                        AccountId = client.Account.AccountId,
                        PrincipalAmount = amount,
                        InterestRate = 0.05m, 
                        LoanStartDate = DateTime.UtcNow,
                        LoanEndDate = DateTime.UtcNow.AddYears(1), 
                        OutstandingAmount = amount * 1.05m 
                    };
                    _context.Loans.Add(loan);
                    break;
                case "EDIT_CUST":
                    if (editCustomerDto != null)
                    {
                        client.FirstName = editCustomerDto.FirstName ?? client.FirstName;
                        client.LastName = editCustomerDto.LastName ?? client.LastName;
                        client.Email = editCustomerDto.Email ?? client.Email;
                        client.PhoneNumber = editCustomerDto.PhoneNumber ?? client.PhoneNumber;
                        client.IDPassport = editCustomerDto.IDPassport ?? client.IDPassport;
                        client.BankAccountNumber = editCustomerDto.BankAccountNumber ?? client.BankAccountNumber;
                        client.DateOfBirth = editCustomerDto.DateOfBirth ?? client.DateOfBirth;
                        client.FingerprintData = editCustomerDto.FingerprintData ?? client.FingerprintData;
                    }
                    break;
                case "CURR_EXCH":
                    if (!string.IsNullOrEmpty(currencyCode))
                    {
                        decimal exchangeRate = await GetExchangeRateAsync(currencyCode); // Simulated exchange rate retrieval
                        decimal convertedAmount = amount * exchangeRate;
                        client.Account.Balance += convertedAmount;
                    }
                    break;
                case "DEL_CUST":
                    var staff = await _context.Staffs.FindAsync(staffId);
                    if (staff == null || !await IsAdminAsync(staff))
                        return false;
                    _context.Clients.Remove(client);
                    break;
                default:
                    return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> IsAdminAsync(Staff staff)
        {
            var roles = await (from ur in _context.UserRoles
                               join r in _context.Roles on ur.RoleId equals r.Id
                               where ur.UserId == staff.Id
                               select r.Name)
                              .ToListAsync();

            return roles.Contains("Admin");
        }

        private async Task<decimal> GetExchangeRateAsync(string currencyCode)
        {
            // Simulate an API call to get exchange rates
            // For demonstration, let's return a dummy exchange rate
            await Task.Delay(100); // Simulate network delay
            return currencyCode switch
            {
                "USD" => 110.0m, // Example rate: 1 USD = 110 KES
                "EUR" => 130.0m, // Example rate: 1 EUR = 130 KES
                _ => 1.0m,
            };
        }
    }
}
