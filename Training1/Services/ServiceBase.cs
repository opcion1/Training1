using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories.Interfaces;
using Training1.Services.Interfaces;

namespace Training1.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : ModelBase
    {
        protected readonly IRepositoryBase<T> _entityRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public ServiceBase(IRepositoryBase<T> repository,
                            IUnitOfWork unitOfWork)
        {
            _entityRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(T entity)
        {
            await _entityRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _entityRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditAsync(T entity)
        {
            _entityRepository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistsEntity(int id)
        {
            return await _entityRepository.Exists(id);
        }

        public T GetById(int id)
        {
            return _entityRepository.GetById(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _entityRepository.ListAsync();
        }
    }
}
