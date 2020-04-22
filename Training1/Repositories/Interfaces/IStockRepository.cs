﻿

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories.Interfaces
{
    public interface IStockRepository : IRepositoryBase<Stock>
    {
        Task<ICollection<Stock>> ListAsyncByProductId(int productId);
    }
}