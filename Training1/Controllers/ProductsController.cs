using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Models.ViewModels;
using Training1.Repositories;

namespace Training1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository,
                                    IAuthorizationService authorizationService,
                                    IConfiguration configuration)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
            _configuration = configuration;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchOrFilter, int? indexPage, string sortOrder)
        {
            int itemsPerPage = _configuration.GetValue<int>("ItemsPerPage");
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Product(), UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                ICollection<Product> products;
                if (NullableEnum.TryParse(searchOrFilter, out ProductCategory? category))
                {
                    products = await _productRepository.ListAsyncByCategory((ProductCategory)category);
                }
                else
                {
                    products = await _productRepository.ListAsync();

                }
                switch (sortOrder)
                {
                    case "name_desc":
                        products = products.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "category":
                        products = products.OrderBy(p => p.Category.ToString()).ToList();
                        break;
                    case "category_desc":
                        products = products.OrderByDescending(p => p.Category.ToString()).ToList();
                        break;
                    default:
                        products = products.OrderBy(p => p.Name).ToList();
                        break;
                }
                
                ProductsViewModel vm = new ProductsViewModel {
                    Products = products
                                .Skip(((indexPage ?? 1) - 1) * itemsPerPage)
                                .Take(itemsPerPage),
                    PageIndex = indexPage ?? 1,
                    TotalItems = products.Count(),
                    CategoryFilter = category,
                    CurrentSort = sortOrder,
                    NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "",
                    CategorySort = sortOrder == "category" ? "category_desc" : "category"
                };
                return View(vm);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                return View(product);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, UserOperations.Create);
                if (isAuthorized.Succeeded) 
                { 
                    await _productRepository.AddAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, UserOperations.Update);
            if (isAuthorized.Succeeded)
            {
                return View(product);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, UserOperations.Update);
                    if (isAuthorized.Succeeded)
                    {
                        await _productRepository.UpdateAsync(product);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                return View(product);
            }
            else 
            { 
                return new ChallengeResult(); 
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Product(), UserOperations.Update);
            if (isAuthorized.Succeeded)
            {
                await _productRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ChallengeResult();
            }
        }

        private bool ProductExists(int id)
        {
            return _productRepository.ProductExists(id);
        }
    }
}
