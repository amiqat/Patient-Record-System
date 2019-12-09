using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Repositories
{
    public interface IRecordRepository : IBaseRepository<Record>
    {
        Task<PagedList<Record>> ListAsync(QueryStringParameters queryString, Dictionary<string, System.Linq.Expressions.Expression<Func<Record, object>>> columnsMap);

        Task<bool> IsExist(int id);
    }
}