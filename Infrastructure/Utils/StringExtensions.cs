using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Utils
{
    public static class StringExtensions
    {
        public static bool IsValidGuid(string input)
        {
            return Guid.TryParse(input, out _);
        }
    }
}
