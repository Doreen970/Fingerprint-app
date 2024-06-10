using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime LoanEndDate { get; set; }
        public decimal OutstandingAmount { get; set; }
    }
}
