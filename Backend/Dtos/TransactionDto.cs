using Backend.Models;

namespace Backend.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string StaffId { get; set; }
        public Staff Staff { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
