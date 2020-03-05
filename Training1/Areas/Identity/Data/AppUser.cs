using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Training1.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        public Status AccountStatus{ get; set; }
    }

    public enum Status
    {
        Submitted,
        Approved,
        Rejected
    }
}
