

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFSesshinRepository : EFRepositoryBase<Sesshin>, ISesshinRepository
    {
        public EFSesshinRepository(ProductContext context) : base(context)
        {
        }
    }
}
