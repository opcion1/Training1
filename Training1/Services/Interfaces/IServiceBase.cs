using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Services.Interfaces
{
    public interface IServiceBase<T> where T : ModelBase
    { 

        Task CreateAsync(T entity);

        Task EditAsync(T entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> ListAsync();

        Task<T> GetByIdAsync(int id);

        T GetById(int id);

        Task<bool> ExistsEntity(int id);
    }
}
