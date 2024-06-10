using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string IDPassport { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte[] FingerprintData { get; set; }
        public Account Account { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
