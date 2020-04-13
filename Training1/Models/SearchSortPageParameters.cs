using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Models
{
    public class SearchSortPageParameters
    {
        public string searchOrFilter { get; set; }
        public int? indexPage { get; set; }
        public string sortOrder { get; set; }
    }

    public class SearchSortPageResult<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
