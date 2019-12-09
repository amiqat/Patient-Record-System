using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Repositories
{
    public interface IMetaDataRepository : IBaseRepository<MetaData>
    {
        Task<MetaDataReport> getMetaStat();
    }
}