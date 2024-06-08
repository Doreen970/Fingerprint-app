using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Device
    {
        [Key]
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public int? StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}
