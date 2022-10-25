using Holding.Base.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Holding.Base.DataProvider
{
    [DbConfigurationType( typeof( DatabaseContextConfiguration ) )]
    internal class DatabaseContext : DbContext , IUnitOfWork
    {       
        public DatabaseContext( )
            : base( ApplicationSettingsFactory.GetApplicationSettings().SqlConnectionString )
        {
            Initialize();         
        }

        public DatabaseContext( DbConnection dbConnection , bool contextOwnsConnection)
            : base( dbConnection , contextOwnsConnection )
        {
            Initialize();         
        }

        private void Initialize()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;

          //  Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            if ( !DataProviderConfigurator.IsConfigured ) return;

            Array.ForEach( DataProviderConfigurator.MappingAssemblies.ToArray() , modelBuilder.AddMappingsFromAssemblyOf );
            Array.ForEach( DataProviderConfigurator.MappingAssemblies.ToArray() , modelBuilder.AddRegisterationFromAssemblyOf );
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }      

        public void BulkInsert<T>( IEnumerable<T> entities )
        {
            throw new NotImplementedException( "NotImplemented" );
        }

        public void BulkDelete<T>( IEnumerable<T> entities )
        {
            throw new NotImplementedException( "NotImplemented" );
        }
    }
}
