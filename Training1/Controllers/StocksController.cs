using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class StocksController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IAuthorizationService _authorizationService;

        public StocksController(IStockService stockService,
                                    IAuthorizationService authorizationService)
        {
            _stockService = stockService;
            _authorizationService = authorizationService;
        }
        
        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _stockService.GetByIdAsync((int)id);

            if (stock == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, stock, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                return View(stock);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Stocks/Create
        public IActionResult Create(int productId)
        {
            ViewData["ProductId"] = productId;
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,UnityType,PricePorUnity,TotalPrice,Currency,ProductId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, stock, UserOperations.Create);
                if (isAuthorized.Succeeded)
                {
                    await _stockService.CreateAsync(stock);
                    return RedirectToAction("Details", "Products", new { id = stock.ProductId, showStock = true });
                }
                else
                {
                    return new ChallengeResult();
                }
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _stockService.GetByIdAsync((int)id);
            if (stock == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, stock, UserOperations.Update);
            if (isAuthorized.Succeeded)
            {
                return View(stock);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,UnityType,PricePorUnity,TotalPrice,Currency,CommandDate,ProductId")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, stock, UserOperations.Update);
                    if (isAuthorized.Succeeded)
                    {
                        await _stockService.EditAsync(stock);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await StockExists(stock.Id);
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Products", new { id = stock.ProductId });
            }

            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _stockService.GetByIdAsync((int)id);
            if (stock == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, stock, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                ViewBag.ProductId = stock.ProductId;
                return View(stock);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int productId)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                await _stockService.DeleteAsync(id);
                return RedirectToAction("Details", "Products", new { id = productId });
            }
            else
            {
                return new ChallengeResult();
            }
        }

        private async Task<bool> StockExists(int id)
        {
            return await _stockService.ExistsEntity(id);
        }
    }
}
