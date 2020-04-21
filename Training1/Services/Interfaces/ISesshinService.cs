using System.Collections.Generic;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Services.Interfaces
{
    public interface ISesshinService : IServiceBase<Sesshin>
    {
        Task<IEnumerable<DayOfSesshin>> GetDaysOfSesshin(int sesshinId);
        Task SetNumberOfPeopleByDayIdAsync(int id, int numberOfPeople);
        string GetSesshinOwner(int sesshinId);
    }
}
