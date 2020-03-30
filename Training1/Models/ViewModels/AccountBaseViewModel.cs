using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Models.ViewModels
{
    public class AccountBaseViewModel : PagingBaseViewModel
    {
        public string SearchStr { get; set; }
        public string CurrentSort { get; set; }
        public string FullNameSort { get; set; }
        public string EmailSort { get; set; }
        public string StatusSort { get; set; }
    }
}
