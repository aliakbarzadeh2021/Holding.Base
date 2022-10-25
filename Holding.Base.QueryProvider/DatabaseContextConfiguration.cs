using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Holding.Base.QueryProvider
{    
    internal class DatabaseContextConfiguration : DbConfiguration
    {
        public DatabaseContextConfiguration()
        {            
            SetProviderServices( SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance );
        }
    }
}
