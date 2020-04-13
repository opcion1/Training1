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
        public IRepositoryBase<T> EntityRepository => throw new NotImplementedException();
    }
}
