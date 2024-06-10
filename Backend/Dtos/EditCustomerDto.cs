namespace Backend.Dtos
{
    public class EditCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IDPassport { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] FingerprintData { get; set; }
    }
}
