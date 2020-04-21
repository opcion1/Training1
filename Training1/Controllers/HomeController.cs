using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training1.Areas.Identity.Data;
using Training1.Authorization;
using Training1.Infrastructure;
using Training1.Models;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHomeService _homeService;
        public HomeController(IAuthorizationService authorizationService,
                                IHomeService homeService)
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
                AppUser user = new AppUser { Id = id };
                if (user != null)
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
                else
                {
                    throw new KeyNotFoundException();
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
