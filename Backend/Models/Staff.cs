using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string RegistrationNo { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PassportID { get; set; } = string.Empty;
        public Privilege Privilege { get; set; }
    }
}
