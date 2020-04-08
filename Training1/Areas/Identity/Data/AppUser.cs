using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Training1.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; }
        public Status AccountStatus{ get; set; }
        public string AppStyle { get; set; }
    }

    public enum Status
    {
        Submitted,
        Approved,
        Rejected
    }
}
