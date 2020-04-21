using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;

namespace Training1.Repositories
{
    public abstract class EFRepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {
        protected readonly ProductContext _context;
        protected readonly DbSet<T> _dbSet;
        public EFRepositoryBase(ProductContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> Entities => _dbSet;

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id); 
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<T> GetFirstOrDefautByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public T GetFirstOrDefaultByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<bool> Exists(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }
    }
}
