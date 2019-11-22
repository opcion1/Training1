using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Training1.Models;

namespace Training1.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Training1.Models.Product> Product { get; set; }
        public DbSet<Training1.Models.Stock> Stock { get; set; }
    }
}
