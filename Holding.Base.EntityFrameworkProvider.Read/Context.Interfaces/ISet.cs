using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;

namespace Holding.Base.EntityFrameworkProvider.Read.Context.Interfaces
{

    public interface ISet<TEntity> where TEntity : DataTableBase
    {
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        IQueryable<TEntity> AsQuery();
        ISet<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> include);
        ISet<TEntity> WithInclude(string include);
    }
}