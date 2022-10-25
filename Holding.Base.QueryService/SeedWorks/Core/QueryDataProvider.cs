using Holding.Base.Infrastructure.Querying;
using Holding.Base.QueryProvider;
using System.Data.Entity;
using System.Linq;

namespace Holding.Base.QueryService.SeedWorks.Core
{
    public class QueryDataProvider : IQueryDataProvider
    {
        public IQueryable<T> Query<T>() where T : class
        {
            return ReadOnlyDatabase.AsReadOnly().Set<T>().AsNoTracking().AsQueryable();            
        }
    }
}
