using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Extensions;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Persistence.Contexts;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Persistence.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Patient> GetById(int id)
        {
            return await _context.Patients.Include(x => x.MetaData).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Patient>> Search(string prfix, int size)
        {
            return await _context.Patients.Where(x => x.PatientName.StartsWith(prfix)).OrderBy(x => x.PatientName).Take(size).ToListAsync();
        }

        public async Task<PatientReportResource> GetPatientReport(int id)
        {
            var PatientDiseaseList = new List<PatientDiseasesResource>();
            var res = await _context.Patients.Select(x => new
            {
                x.Id,
                x.PatientName,
                Diseases = x.Records.Select(r => r.DiseaseName).Distinct()
            }).ToListAsync();
            var Patient = res.FirstOrDefault(x => x.Id == id);
            if (Patient.Diseases.Count() > 1)
            {
                foreach (var item in res.Where(x => x.Id != id).ToList())
                {
                    if (item.Diseases.Count() < 2)
                        continue;
                    var commonDiseases = Patient.Diseases.Intersect(item.Diseases).ToList();
                    if (commonDiseases.Count() < 2)
                        continue;
                    PatientDiseaseList.Add(new PatientDiseasesResource
                    {
                        PatientName = item.PatientName,
                        DiseaseList = commonDiseases
                    });
                }
            }
            PatientDiseaseList = PatientDiseaseList.OrderBy(x => x.PatientName).ToList();
            var list = await _context.PatientReport.Select(x => new PatientReportResource
            {
                Id = x.Id,
                PatientName = x.PatientName,
                Age = x.Age,
                Avg = x.Avg,
                AvgW = x.AvgW,
                MostVisitMounth = x.MostVisitMounth,
                PatientDiseases = PatientDiseaseList,
                record = _context.Records.Where(x => x.PatientId == id).OrderBy(x => x.TimeOfEntry).Skip(4).Take(1).Select(x => new RecordResource
                {
                    Id = x.Id,
                    PatientName = x.Patient.PatientName,
                    DiseaseName = x.DiseaseName,
                    TimeOfEntry = x.TimeOfEntry
                }).FirstOrDefault()
            }).SingleOrDefaultAsync(x => x.Id == id);
            return list;
        }

        public async Task<PagedList<PatientResource>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<PatientResource, object>>> columnsMap)
        {
            var query = _context.Patients.Select(x => new PatientResource
            {
                Id = x.Id,
                PatientName = x.PatientName,
                DateOfBirth = x.DateOfBirth,
                MetaDataCount = x.MetaData.Count(),
                LastEntry = x.Records.Select(x => x.TimeOfEntry).OrderByDescending(x => x).FirstOrDefault()
            }).Sort<PatientResource>(queryString.SortBy, columnsMap, queryString.IsSortAscending);

            return await PagedList<PatientResource>.ToPagedListAsync(query, queryString.PageNumber, queryString.PageSize);
        }

        public async Task<bool> IsExist(int Oid, int? id)
        {
            var query = id.HasValue ? _context.Patients.Where(x => x.Id != id) : _context.Patients;

            return await query.AnyAsync(e => e.OffcialId == Oid);
        }
    }
}