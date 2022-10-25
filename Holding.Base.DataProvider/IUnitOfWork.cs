using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Holding.Base.DataProvider
{
    public interface IUnitOfWork : IDisposable
    {
        Database Database { get; }
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T model) where T:class ;
        int SaveChanges();
    }
}
