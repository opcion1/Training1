

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        public EFProductRepository(ProductContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Product;

        public async Task<List<Product>> ListAsync()
        {
            return await Products.ToListAsync();
        }
        public async Task<List<Product>> ListAsyncByCategory(ProductCategory category)
        {
            return await Products.Where(p => p.Category == category).ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public bool ProductExists(int id)
        {
            return Products.Any(e => e.Id == id);
        }
    }
}
