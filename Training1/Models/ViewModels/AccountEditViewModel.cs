using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training1.Areas.Identity.Data;

namespace Training1.Models.ViewModels
{
    public class AccountEditViewModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountEditViewModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            Roles = GetItemsRole();
        }
        public AppUser Account { get; set; }
        
        [Display(Name ="Role")]
        public string CurrentRole { get; set; }
        public List<SelectListItem> Roles { get; set; }

        private List<SelectListItem> GetItemsRole()
        {
            return _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name }).ToList();
        }
    }
}
