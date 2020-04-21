using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface IStockService : IServiceBase<Stock>
    {
        Task<IEnumerable<Stock>> GetStocksAsync(int productId);
    }
}
