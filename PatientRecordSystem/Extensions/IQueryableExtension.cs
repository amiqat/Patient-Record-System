using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string sortBy, Dictionary<string, Expression<Func<T, object>>> columnsMap, bool IsSortAscending = true)
        {
            if (query == null)
                throw new ArgumentNullException("source");

            if (String.IsNullOrWhiteSpace(sortBy) || !columnsMap.ContainsKey(sortBy))
                return query;

            if (IsSortAscending)
                query = query.OrderBy(columnsMap[sortBy]);
            else
                query = query.OrderByDescending(columnsMap[sortBy]);

            return query;
        }
    }
}