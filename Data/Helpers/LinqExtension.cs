using HRIS.Core.Entity;
using System.Linq;

namespace HRIS.Data.Helpers
{
    public static class LinqExtension
    {
        public static IQueryable<T> IsActiveRows<T>(this IQueryable<T> source) where T : BaseEntity
        {
            return source.Where(x => x.IsActive);
        }
    }
}
