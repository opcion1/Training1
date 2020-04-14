using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private ProductContext _productContext;

        public UnitOfWork(ProductContext context)
        {

            _productContext = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _productContext.SaveChangesAsync();
        }
    }
}
