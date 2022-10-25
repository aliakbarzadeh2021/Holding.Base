using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repositories
{
    public interface IRepository<T> where T :class
    {
        void Add(T data);
        void Remove(Guid dataId);
        T FindBy(Guid id);
        void Save(T data);
        void SaveAll(IEnumerable<T> data);
        IQueryable<T> FindAll();
    }
}