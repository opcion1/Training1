using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Repositories;

namespace Training1.ViewComponents
{
    public class SesshinDaysViewComponent : ViewComponent
    {
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;
        public SesshinDaysViewComponent(IDayOfSesshinRepository dayOfSesshinRepository)
        {
            _dayOfSesshinRepository = dayOfSesshinRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int sesshinId)
        {
            var daysOfSesshin = await _dayOfSesshinRepository.ListAsync(sesshinId);

            return View(daysOfSesshin);
        }
    }
}
