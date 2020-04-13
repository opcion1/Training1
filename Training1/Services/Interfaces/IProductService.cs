using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface IProductService
    {
        IProductRepository Product { get; }

        Task<IEnumerable<Product>> ListAsyncByCategory(ProductCategory category);

        Task<SearchSortPageResult<Product>> SearchSortAndPageProductAll(SearchSortPageParameters parameters);

        Task CreateAsync(Product product);

        Task EditAsync(Product product);

        Task DeleteAsync(int id);
    }
}
