using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface IProductService : IServiceBase<Product>
    {
        Task<IEnumerable<Product>> ListAsyncByCategory(ProductCategory category);

        Task<SearchSortPageResult<Product>> SearchSortAndPageProductAll(SearchSortPageParameters parameters);
    }
}
