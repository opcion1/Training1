using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;
using Training1.Infrastructure;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class HomeService : IHomeService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeService(UserManager<AppUser> userManager,
                                IAccountRepository accountRepository,
                                IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _accountRepository= accountRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task UpdateAppStyle(string id, string appStyle)
        {
            await _accountRepository.UpdateAppStyle(id, appStyle);
            _contextAccessor.HttpContext.Session.Set<string>("AppStyle", appStyle);
        }
    }
}
