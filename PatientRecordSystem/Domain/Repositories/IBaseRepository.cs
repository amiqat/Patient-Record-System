using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Filter(Func<T, bool> predicate);

        Task<T> GetById(int id);

        Task Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}