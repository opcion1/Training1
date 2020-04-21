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
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAuthorizationService _authorizationService;

        public ProductsController(IProductService productService,
                                    IAuthorizationService authorizationService)
        {
            _productService = productService;
            _authorizationService = authorizationService;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchOrFilter, int? indexPage, string sortOrder)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Product(), UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                SearchSortPageResult<Product> searchSortPageResult = await _productService.SearchSortAndPageProductAll(new SearchSortPageParameters { indexPage = indexPage, searchOrFilter = searchOrFilter, sortOrder = sortOrder });
                                
                ProductsViewModel vm = new ProductsViewModel {
                    Products = searchSortPageResult.Entities,
                    PageIndex = indexPage ?? 1,
                    TotalItems = searchSortPageResult.TotalItems,
                    ItemsPerPage = searchSortPageResult.ItemsPerPage,
                    CategoryFilter = searchOrFilter,
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

            var product = await _productService.GetByIdAsync((int)id);
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
                    await _productService.CreateAsync(product);
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

            var product = await _productService.GetByIdAsync((int)id);
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
                        await _productService.EditAsync(product);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exist = await ProductExists(product.Id);
                    if (!exist)
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

            var product = await _productService.GetByIdAsync((int)id);
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
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Product(), UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                await _productService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ChallengeResult();
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productService.ExistsEntity(id);
        }
    }
}
