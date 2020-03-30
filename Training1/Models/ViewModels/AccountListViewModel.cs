

using System.Collections.Generic;
using Training1.Areas.Identity.Data;

namespace Training1.Models.ViewModels
{
    public class AccountListViewModel : AccountBaseViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
    }
}
