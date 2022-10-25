using System.Threading.Tasks;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;

namespace Holding.Base.EntityFrameworkProvider.Write.Context.Interfaces
{
    public interface IDataContext<TDataTable> where TDataTable : DataTableBase
    {
        //DataSet<TDataTable> Set<TEntity>() where TEntity: DataTableBase;
        ISet<TEntity> Set<TEntity>() where TEntity : DataTableBase;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
        
    }
}