using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Models.ViewModels
{
    public class AccountViewModel
    {
        public ActiveTab ActiveTab { get; set; }
        public string SearchStr { get; set; }
    }
    public enum ActiveTab
    {
        Accounts,
        Roles
    }
}
