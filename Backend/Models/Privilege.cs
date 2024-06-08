using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Privilege
    {
        [Key]
        public int PrivilegeID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
