
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _configuration;
        public IProductRepository Product { get { return _productRepository; } }

        public ProductService(IProductRepository productRepository,
                                    IUnitOfWork unitOfWork,
                                    IConfiguration configuration)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> ListAsyncByCategory(ProductCategory category)
        {
            return await _productRepository.GetByConditionAsync(p => p.Category == category);
        }

        public async Task<SearchSortPageResult<Product>> SearchSortAndPageProductAll(SearchSortPageParameters parameters)
        {
            SearchSortPageResult<Product> result = new SearchSortPageResult<Product>();
            result.ItemsPerPage = _configuration.GetValue<int>("ItemsPerPage");
            IEnumerable<Product> products;
            if (NullableEnum.TryParse(parameters.searchOrFilter, out ProductCategory? category))
            {
                products = await ListAsyncByCategory((ProductCategory)category);
            }
            else
            {
                products = await _productRepository.ListAsync();

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

        public async Task CreateAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditAsync(Product product)
        {
            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
