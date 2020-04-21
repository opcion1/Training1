using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public class EFFoodRepository : EFRepositoryBase<Food>, IFoodRepository
    {
        public EFFoodRepository(ProductContext productContext)
            :base(productContext)
        {
        }
    }
}
