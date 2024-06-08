namespace Backend.Dtos
{
    public class ClientDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string IDPassport { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte[] FingerprintData { get; set; }
    }
}
