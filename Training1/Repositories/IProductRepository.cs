﻿

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Task<List<Product>> ListAsync();
        Task<List<Product>> ListAsyncByCategory(ProductCategory category);
        Task<Product> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);

        bool ProductExists(int id);
    }
}