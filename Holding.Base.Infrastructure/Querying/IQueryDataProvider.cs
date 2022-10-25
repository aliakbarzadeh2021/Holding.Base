using System.Linq;

namespace Holding.Base.Infrastructure.Querying
{
    public interface IQueryDataProvider
    {
        IQueryable<T> Query<T>() where T : class;
    }
}
