using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Receipt
    {
        [Key]
        public int TransactionId { get; set; }
        public string ClientName { get; set; }
        public string ServiceName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string BankAccountNumber { get; set; }
        public string ServedBy { get; set; }
    }
}
