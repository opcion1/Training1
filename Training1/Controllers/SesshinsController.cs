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
    [Authorize]
    public class SesshinsController : Controller
    {
        private readonly ISesshinService _sesshinService;
        private readonly IAuthorizationService _authorizationService;

        public SesshinsController(ISesshinService sesshinService,
                                    IAuthorizationService authorizationService)
        {
            _sesshinService = sesshinService;
            _authorizationService = authorizationService;
        }

        // GET: Sesshins
        public async Task<IActionResult> Index()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Sesshin(), UserOperations.Read);
            if (isAuthorized.Succeeded)
            {

                return View(await _sesshinService.Sesshin.ListAsync());
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Sesshins/Details/5
        public async Task<IActionResult> Details(int? id, int? mealId)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var sesshin = await _sesshinService.Sesshin.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                ViewData["Id"] = mealId ?? -1;
                return View(sesshin);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Sesshins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sesshins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,AppUserId")] Sesshin sesshin)
        {
            if (ModelState.IsValid)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Create);
                if (isAuthorized.Succeeded)
                {
                    await _sesshinService.CreateAsync(sesshin);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            return View(sesshin);
        }

        // GET: Sesshins/Edit/5
        public async Task<IActionResult> Edit(int? id, bool? fromDetail)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var sesshin = await _sesshinService.Sesshin.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Update);
            if (isAuthorized.Succeeded)
            {
                ViewData["FromDetail"] = fromDetail ?? false;
                return View(sesshin);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Sesshins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,AppUserId")] Sesshin sesshin)
        {
            if (id != sesshin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Update);
                    if (isAuthorized.Succeeded)
                    {
                        await _sesshinService.EditAsync(sesshin);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await SesshinExists(id);
                    if (!exists)
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
            return View(sesshin);
        }

        // GET: Sesshins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var sesshin = await _sesshinService.Sesshin.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                return View(sesshin);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: Sesshins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesshin = await _sesshinService.Sesshin.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                await _sesshinService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ChallengeResult();
            }
        }

        private async Task<bool> SesshinExists(int id)
        {
            return await _sesshinService.Sesshin.Exists(id);
        }
    }
}
