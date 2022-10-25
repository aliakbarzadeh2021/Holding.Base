using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;

namespace Holding.Base.EntityFrameworkProvider.Read.Context.Interfaces
{
    public interface IReadOnlyContext<TDataTable> where TDataTable : DataTableBase
    {
        ISet<TEntity> Set<TEntity>() where TEntity : DataTableBase;

        IQueryable<TDto> SqlQuery<TDto>(string storedProcedure, Dictionary<string, object> parameteres) where TDto : class;
    }
}
