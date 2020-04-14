using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
