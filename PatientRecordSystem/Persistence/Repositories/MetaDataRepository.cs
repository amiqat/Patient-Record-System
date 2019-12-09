using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Persistence.Contexts;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Persistence.Repositories
{
    public class MetaDataRepository : BaseRepository<MetaData>, IMetaDataRepository
    {
        public MetaDataRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<MetaDataReport> getMetaStat()
        {
            return await _context.metaDataStats.Select(x => new MetaDataReport
            {
                AveragePerPatient = x.AveragePerPatient,
                MaxPerPatient = x.MaxPerPatient,
                TopMetaData = _context.TopMetaData.ToList()
            }).SingleOrDefaultAsync();
        }
    }
}