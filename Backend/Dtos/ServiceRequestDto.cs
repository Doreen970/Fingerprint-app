namespace Backend.Dtos
{
    public class ServiceRequestDto
    {
        public int ClientId { get; set; }
        public string StaffId { get; set; }
        public int ServiceId { get; set; }
        public decimal Amount { get; set; }
    }
}
