using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Services.Communication;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Services
{
    public interface IRecordService
    {
        Task<PagedList<Record>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<Record, object>>> columnsMap);

        Task<RecordResponse> SaveAsync(Record record);

        Task<RecordResponse> UpdateAsync(int id, Record record);

        Task<RecordResponse> GetById(int id);
    }
}