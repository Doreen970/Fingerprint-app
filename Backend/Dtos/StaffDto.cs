namespace Backend.Dtos
{
    public class StaffDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RegistrationNo { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PassportID { get; set; } = string.Empty;
    }
}
