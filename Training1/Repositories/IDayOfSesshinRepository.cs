using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public interface IDayOfSesshinRepository
    {        
        Task<ICollection<DayOfSesshin>> ListAsync(int sesshinId);
    }
}
