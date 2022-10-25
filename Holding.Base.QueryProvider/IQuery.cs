using System.Data.Entity;
using System.Linq;

namespace Holding.Base.QueryProvider
{
    public interface IQuery
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        Database Database { get; }
    }
}
