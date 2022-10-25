
using Holding.Base.Infrastructure.Querying;

namespace Holding.Base.QueryService.SeedWorks.Core
{
    public class QueryServiceBase
    {
        private readonly IQueryDataProvider _queryProvider;

        public QueryServiceBase(IQueryDataProvider connectionProvider)
        {
            _queryProvider = connectionProvider;
        }
        protected IQueryDataProvider QueryProvider
        {
            get { return _queryProvider; }
        }
    }
}
