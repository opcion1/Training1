using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Services.Interfaces
{
    public interface IHomeService
    {
        Task UpdateAppStyle(string id, string appStyle);
    }
}
