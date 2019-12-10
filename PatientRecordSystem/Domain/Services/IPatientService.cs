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
    public interface IPatientService
    {
        Task<PatientResponse> GetById(int id);

        Task<IEnumerable<Patient>> Search(string prfix, int size);

        Task<PatientReportResource> GetPatientReport(int id);

        Task<PagedList<PatientResource>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<PatientResource, object>>> columnsMap);

        Task<PatientResponse> SaveAsync(Patient patient);

        Task<PatientResponse> UpdateAsync(int id, SavePatientResource savePatient);

        Task<bool> IsExist(int Oid, int? id);
    }
}