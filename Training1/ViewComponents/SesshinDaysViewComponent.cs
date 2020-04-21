using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Repositories;
using Training1.Services.Interfaces;

namespace Training1.ViewComponents
{
    public class SesshinDaysViewComponent : ViewComponent
    {
        private readonly ISesshinService _sesshinService;
        public SesshinDaysViewComponent(ISesshinService sesshinService)
        {
            _sesshinService = sesshinService;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int sesshinId)
        {
            var daysOfSesshin = await _sesshinService.GetDaysOfSesshin(sesshinId);

            return View(daysOfSesshin);
        }
    }
}
