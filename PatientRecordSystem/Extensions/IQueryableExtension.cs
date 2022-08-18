using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string sortBy, Dictionary<string, Expression<Func<T, object>>> columnsMap, bool isSortAscending = true)
        {
            if (query == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrWhiteSpace(sortBy) || !columnsMap.ContainsKey(sortBy))
                return query;

            query = isSortAscending ? query.OrderBy(columnsMap[sortBy]) : query.OrderByDescending(columnsMap[sortBy]);

            return query;
        }
    }
}