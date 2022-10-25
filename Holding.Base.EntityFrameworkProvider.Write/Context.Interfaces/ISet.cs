using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;

namespace Holding.Base.EntityFrameworkProvider.Write.Context.Interfaces
{

    public interface ISet<TEntity> where TEntity : DataTableBase
    {
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        IQueryable<TEntity> AsQuery();
        ISet<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> include);
        ISet<TEntity> WithInclude(string include);


        TEntity Attach(TEntity entity);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
    }
}