namespace Backend.Interfaces
{
    public interface IAccountMaskingService
    {
        string MaskBankAccountNumber(string accountNumber);
    }
}
