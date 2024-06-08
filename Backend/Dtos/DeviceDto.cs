namespace Backend.Dtos
{
    public class DeviceDto
    {
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public string? StaffID { get; set; }
    }
}
