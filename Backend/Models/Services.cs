using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
