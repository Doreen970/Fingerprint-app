using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
