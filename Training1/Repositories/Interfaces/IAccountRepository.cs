using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models;

namespace Training1.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IQueryable<AppUser> Users { get; }
        Task<ICollection<AppUser>> ListAsync();
        Task<ICollection<IdentityRole>> ListRolesAsync();
        Task UpdateAccountStatus(string id, Status status);
        Task UpdateAppStyle(string id, string appStyle);
    }
}
