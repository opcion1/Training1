using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Models;

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
    }
}
