using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Training1.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> Entities { get; }

        Task<IEnumerable<T>> ListAsync();

        Task<T> GetByIdAsync(int id);

        T GetById(int id);

        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);

        Task<T> GetFirstOrDefautByConditionAsync(Expression<Func<T, bool>> expression);

        T GetFirstOrDefaultByCondition(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);
        void Update(T entity);

        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}
