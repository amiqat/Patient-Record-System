using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<PatientReportResource> GetPatientReport(int id);

        Task<IEnumerable<Patient>> Search(string prfix, int size);

        Task<PagedList<PatientResource>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<PatientResource, object>>> columnsMap);

        Task<bool> IsExist(int Oid, int? id);
    }
}