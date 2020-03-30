

using System.Collections.Generic;
using Training1.Areas.Identity.Data;

namespace Training1.Models.ViewModels
{
    public class AccountListViewModel : PagingViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
        public string SearchStr { get; set; }
        public string CurrentSort { get; set; }
        public string FullNameSort { get; set; }
        public string EmailSort { get; set; }
        public string RoleSort { get; set; }
    }
}
