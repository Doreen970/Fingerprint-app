using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal Balance { get; set; }
    }
}
