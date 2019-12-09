using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training1.Authorization;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;

        public ProductsController(IProductRepository productRepository,
                                    IAuthorizationService authorizationService)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
        }

        // GET: Products
        public async Task<IActionResult> Index(ProductCategory? category)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Product(), UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                if (category.HasValue)
                {
                    return View(await _productRepository.ListAsyncByCategory((ProductCategory)category));
                }
                return View(await _productRepository.ListAsync());
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id, bool? showStock)
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
                ViewData["ShowStock"] = showStock ?? false;
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
