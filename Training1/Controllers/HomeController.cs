using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Models;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService,
                                IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _homeService = homeService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAppStyle(string id, string appStyle)
        {
            try
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, new AppUser { Id = id }, UserOperations.Update);
                if (isAuthorized.Succeeded)
                {
                    await _homeService.UpdateAppStyle(id, appStyle);
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            catch (KeyNotFoundException)
            {
                ModelState.AddModelError("", "User Not Found");
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }
    }
}
