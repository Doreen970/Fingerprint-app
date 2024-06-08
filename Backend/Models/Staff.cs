using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Staff : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RegistrationNo { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PassportID { get; set; } = string.Empty;
        //public int PrivilegeId { get; set; }
        //public Privilege Privilege { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
