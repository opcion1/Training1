
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Configuration;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        private readonly IGridConfiguration _gridConfiguration;

        public ProductService(IProductRepository productRepository,
                                    IUnitOfWork unitOfWork,
                                    IGridConfiguration gridConfiguration)
            : base(productRepository, unitOfWork)
        {
            _gridConfiguration = gridConfiguration;
        }

        public async Task<IEnumerable<Product>> ListAsyncByCategory(ProductCategory category)
        {
            return await _entityRepository.GetByConditionAsync(p => p.Category == category);
        }

        public async Task<SearchSortPageResult<Product>> SearchSortAndPageProductAll(SearchSortPageParameters parameters)
        {
            SearchSortPageResult<Product> result = new SearchSortPageResult<Product>();
            result.ItemsPerPage = _gridConfiguration.ItemsPerPage;
            IEnumerable<Product> products;
            if (NullableEnum.TryParse(parameters.searchOrFilter, out ProductCategory? category))
            {
                products = await ListAsyncByCategory((ProductCategory)category);
            }
            else
            {
                products = await this.ListAsync();

            }
            result.TotalItems = products.Count();
            switch (parameters.sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "category":
                    products = products.OrderBy(p => p.Category.ToString());
                    break;
                case "category_desc":
                    products = products.OrderByDescending(p => p.Category.ToString());
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }
            result.Entities = products
                                .Skip(((parameters.indexPage ?? 1) - 1) * result.ItemsPerPage)
                                .Take(result.ItemsPerPage);

            return result;
        }
        
    }
}
