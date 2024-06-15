using Backend.Data;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Backend.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountMaskingService _accountMaskingService;

        public TransactionRepository(ApplicationDbContext context, IAccountMaskingService accountMaskingService)
        {
            _context = context;
            _accountMaskingService = accountMaskingService;
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        public async Task<Staff> GetStaffByIdAsync(string staffId)
        {
            return await _context.Staffs.FindAsync(staffId);
        }

        public async Task<Transaction> AuthorizeTransactionAsync(Transaction transaction, byte[] fingerprint)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientID == transaction.ClientId);
                if (client == null)
                    return null;

                // Verify the fingerprint
                bool isFingerprintValid = VerifyFingerprint(client.FingerprintData, fingerprint);
                if (!isFingerprintValid)
                    return null;

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

        public async Task<Receipt> GenerateReceipt(int transactionId)
        {
            var authorizedTransaction = await _context.Transactions
                .Include(t => t.Client)
                .Include(t => t.Staff)
                .Include(t => t.Service) 
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (authorizedTransaction == null)
                return null;

            string maskedAccountNumber = _accountMaskingService.MaskBankAccountNumber(authorizedTransaction.Client.BankAccountNumber);

            var receipt = new Receipt
            {
                TransactionId = authorizedTransaction.Id,
                ClientName = $"{authorizedTransaction.Client.FirstName} {authorizedTransaction.Client.LastName}",
                BankAccountNumber = maskedAccountNumber,
                ServedBy = $"{authorizedTransaction.Staff.FirstName} {authorizedTransaction.Staff.LastName}",
                ServiceName = authorizedTransaction.Service.ServiceName,
                Amount = authorizedTransaction.Amount,
                Timestamp = authorizedTransaction.Timestamp
            };

            return receipt;
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

        //public async Task<bool> ProcessServiceAsync(int clientId, string staffId, int serviceId, decimal amount, byte[] fingerprint, EditCustomerDto editCustomerDto = null, string currencyCode = null)
        //{
        //    var client = await _context.Clients.Include(c => c.Account).FirstOrDefaultAsync(c => c.ClientID == clientId);
        //    if (client == null) return false;

        //    if (!VerifyFingerprint(client.FingerprintData, fingerprint)) return false;

        //    var service = await _context.Services.FindAsync(serviceId);
        //    if (service == null) return false;

        //    var transaction = new Transaction
        //    {
        //        ClientId = clientId,
        //        StaffId = staffId,
        //        ServiceID = serviceId,
        //        Amount = amount,
        //        Timestamp = DateTime.UtcNow
        //    };

        //    _context.Transactions.Add(transaction);

        //    switch (service.ServiceCode)
        //    {
        //        case "CASH_DEP":
        //            client.Account.Balance += amount;
        //            break;
        //        case "CASH_WDL":
        //            if (client.Account.Balance >= amount)
        //            {
        //                client.Account.Balance -= amount;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //            break;
        //        case "LOAN_DISB":
        //            client.Account.Balance += amount;
        //            var loan = new Loan
        //            {
        //                ClientId = clientId,
        //                AccountId = client.Account.AccountId,
        //                PrincipalAmount = amount,
        //                InterestRate = 0.05m,
        //                LoanStartDate = DateTime.UtcNow,
        //                LoanEndDate = DateTime.UtcNow.AddYears(1),
        //                OutstandingAmount = amount * 1.05m
        //            };
        //            _context.Loans.Add(loan);
        //            break;
        //        case "EDIT_CUST":
        //            if (editCustomerDto != null)
        //            {
        //                client.FirstName = editCustomerDto.FirstName ?? client.FirstName;
        //                client.LastName = editCustomerDto.LastName ?? client.LastName;
        //                client.Email = editCustomerDto.Email ?? client.Email;
        //                client.PhoneNumber = editCustomerDto.PhoneNumber ?? client.PhoneNumber;
        //                client.IDPassport = editCustomerDto.IDPassport ?? client.IDPassport;
        //                client.BankAccountNumber = editCustomerDto.BankAccountNumber ?? client.BankAccountNumber;
        //                client.DateOfBirth = editCustomerDto.DateOfBirth ?? client.DateOfBirth;
        //                client.FingerprintData = editCustomerDto.FingerprintData ?? client.FingerprintData;
        //            }
        //            break;
        //        case "CURR_EXCH":
        //            if (!string.IsNullOrEmpty(currencyCode))
        //            {
        //                decimal exchangeRate = await GetExchangeRateAsync(currencyCode); // Simulated exchange rate retrieval
        //                decimal convertedAmount = amount * exchangeRate;
        //                client.Account.Balance += convertedAmount;
        //            }
        //            break;
        //        case "DEL_CUST":
        //            var staff = await _context.Staffs.FindAsync(staffId);
        //            if (staff == null || !await IsAdminAsync(staff))
        //                return false;
        //            _context.Clients.Remove(client);
        //            break;
        //        default:
        //            return false;
        //    }

        //    await _context.SaveChangesAsync();
        //    return true;
        //}
        public async Task<(bool Success, Transaction Transaction, Client Client, Staff Staff)> ProcessServiceAsync(
    int clientId, string staffId, int serviceId, decimal amount, byte[] fingerprint, EditCustomerDto editCustomerDto = null, string currencyCode = null)
        {
            var client = await _context.Clients.Include(c => c.Account).FirstOrDefaultAsync(c => c.ClientID == clientId);
            if (client == null) return (false, null, null, null);

            if (!VerifyFingerprint(client.FingerprintData, fingerprint)) return (false, null, null, null);

            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff == null) return (false, null, null, null);

            var service = await _context.Services.FindAsync(serviceId);
            if (service == null) return (false, null, null, null);

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
                        return (false, null, null, null);
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
                    }
                    break;
                case "CURR_EXCH":
                    if (!string.IsNullOrEmpty(currencyCode))
                    {
                        decimal exchangeRate = await GetExchangeRateAsync(currencyCode); // implement exchange rate retrieval method
                        decimal convertedAmount = amount * exchangeRate;
                        client.Account.Balance += convertedAmount;
                    }
                    break;
                case "DEL_CUST":
                    if (!await IsAdminAsync(staff))
                        return (false, null, null, null);
                    _context.Clients.Remove(client);
                    break;
                default:
                    return (false, null, null, null);
            }

            await _context.SaveChangesAsync();
            return (true, transaction, client, staff);
        }


        private bool VerifyFingerprint(byte[] storedFingerprintData, byte[] providedFingerprintData)
        {
            // Add actual fingerprint verification logic here
            if (storedFingerprintData == null || providedFingerprintData == null)
                return false;

            return storedFingerprintData.SequenceEqual(providedFingerprintData);
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
            // API call to get exchange rates to be implemented
            await Task.Delay(100); // network delay
            return currencyCode switch
            {
                "USD" => 110.0m, 
                "EUR" => 130.0m, 
                _ => 1.0m,
            };
        }
    }
}
