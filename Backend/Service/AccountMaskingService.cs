
using Backend.Interfaces;

namespace Backend.Service
{
    public class AccountMaskingService : IAccountMaskingService
    {
        public string MaskBankAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || accountNumber.Length < 4)
                return accountNumber;

            var lastFourDigits = accountNumber.Substring(accountNumber.Length - 4);
            return $"****-****-****-{lastFourDigits}";
        }
    }
}
