using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Infrastructure
{
    public interface IEnumUtil
    {
        IEnumerable<SelectListItem> GenerateListEnumWithFriendlyNames<T>() where T : struct, IConvertible;
    }
}
