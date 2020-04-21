using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories.Interfaces
{
    public interface IDayOfSesshinRepository : IRepositoryBase<DayOfSesshin>
    {        
        Task<IEnumerable<DayOfSesshin>> ListAsyncBySesshinId(int sesshinId);
        Task UpdateNumberOfPeopleAsync(int id, int numberOfPeople);
    }
}
