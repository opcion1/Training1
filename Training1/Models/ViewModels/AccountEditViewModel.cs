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
        public AccountEditViewModel()
        {
        }
        public AppUser Account { get; set; }
        
        [Display(Name ="Role")]
        public string CurrentRole { get { return Roles[0].Text; } }
        public List<SelectListItem> Roles { get; set; }

    }
}
