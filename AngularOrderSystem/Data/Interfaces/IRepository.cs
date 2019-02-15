using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AngularOrderSystem.Data.Interfaces
{
    public interface IRepository<T>
    {
        Task<string> Add(T item);
        Task Update(T item);
        Task<bool> Delete(string Id);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);
    }
}
