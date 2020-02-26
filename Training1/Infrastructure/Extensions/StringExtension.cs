using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training1.Infrastructure
{
    public static class StringExtension
    {
        public static bool ContainInsensitive(this string str, string substring)
        {
            if (substring == null)
            {
                throw new ArgumentNullException("substring", "substring cannot be null");
            }
            return str.ToLower().Contains(substring.ToLower());
        }
    }
}
