using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFAccountRepository : IAccountRepository
    {
        private readonly AppIdentityContext _identityContext;
        public EFAccountRepository(AppIdentityContext identityContext)
        {
            _identityContext = identityContext;
        }
        public IQueryable<AppUser> Users => _identityContext.Users;
        public IQueryable<IdentityRole> Roles => _identityContext.Roles;
        
        public async Task<ICollection<AppUser>> ListAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<ICollection<IdentityRole>> ListRolesAsync()
        {
            return await Roles.ToListAsync();
        }

        public async Task UpdateAccountStatus(string id, Status status)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user!= null)
            {
                user.AccountStatus = status;
                user.LockoutEnabled = (status == Status.Rejected);
                if (user.LockoutEnabled)
                {
                    user.LockoutEnd = DateTime.Now.AddYears(100);
                }
                await _identityContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task UpdateAppStyle(string id, string appStyle)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.AppStyle = appStyle;
                await _identityContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
