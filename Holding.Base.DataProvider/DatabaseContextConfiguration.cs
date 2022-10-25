using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Holding.Base.DataProvider
{    
    public class DatabaseContextConfiguration : DbConfiguration
    {
        public DatabaseContextConfiguration()
        {            
            SetProviderServices( SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance );
        }
    }
}
