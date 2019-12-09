using AutoMapper.QueryableExtensions;
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
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Record>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<Record, object>>> columnsMap)
        {
            var query = _context.Records.Include(x => x.Patient).Sort<Record>(queryString.SortBy, columnsMap, queryString.IsSortAscending);

            return await PagedList<Record>.ToPagedListAsync(query, queryString.PageNumber, queryString.PageSize);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Records.AnyAsync(e => e.Id == id);
        }
    }
}