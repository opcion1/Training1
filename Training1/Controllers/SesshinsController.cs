using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Training1.Authorization;
using Training1.Models;
using Training1.Repositories;

namespace Training1.Controllers
{
    [Authorize]
    public class SesshinsController : Controller
    {
        private readonly ISesshinRepository _sesshinRepository;
        private readonly IAuthorizationService _authorizationService;

        public SesshinsController(ISesshinRepository sesshinRepository,
                                    IAuthorizationService authorizationService)
        {
            _sesshinRepository = sesshinRepository;
            _authorizationService = authorizationService;
        }

        // GET: Sesshins
        public async Task<IActionResult> Index()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Sesshin(), UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                return View(await _sesshinRepository.ListAsync());
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: Sesshins/Details/5
        public async Task<IActionResult> Details(int? id, bool? showStock)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var sesshin = await _sesshinRepository.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Read);
            if (isAuthorized.Succeeded)
            {
                ViewData["ShowStock"] = showStock ?? false;
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
                    await _sesshinRepository.AddAsync(sesshin);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var sesshin = await _sesshinRepository.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Update);
            if (isAuthorized.Succeeded)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("SesshinId,Name,Description,StartDate,EndDate,AppUserId")] Sesshin sesshin)
        {
            if (id != sesshin.SesshinId)
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
                        await _sesshinRepository.UpdateAsync(sesshin);
                    }
                    else
                    {
                        return new ChallengeResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesshinExists(sesshin.SesshinId))
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

            var sesshin = await _sesshinRepository.GetByIdAsync((int)id);
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
            var sesshin = await _sesshinRepository.GetByIdAsync((int)id);
            if (sesshin == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, sesshin, UserOperations.Delete);
            if (isAuthorized.Succeeded)
            {
                await _sesshinRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ChallengeResult();
            }
        }

        private bool SesshinExists(int id)
        {
            return _sesshinRepository.SesshinExists(id);
        }
    }
}
