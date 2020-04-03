

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Training1.Repositories;

namespace Training1.Controllers
{
    [Authorize]
    public class DayOfSesshinsController : Controller
    {
        private readonly IDayOfSesshinRepository _dayOfSesshinRepository;
        public DayOfSesshinsController(IDayOfSesshinRepository dayOfSesshinRepository)
        {
            _dayOfSesshinRepository = dayOfSesshinRepository;
        }


        // POST: DayOfSesshins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateNumberOfPeople(int id, int numberOfPeople)
        {
            if (ModelState.IsValid)
            {
                await _dayOfSesshinRepository.UpdateNumberOfPeopleAsync(id, numberOfPeople);
            }

            return Ok();
        }
    }
}
