using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.QueryProvider
{
	[DbConfigurationType(typeof(Holding.Base.DataProvider.DatabaseContextConfiguration))]
    internal class ReadOnlyDatabaseContext : DbContext, IQuery
    {
        public ReadOnlyDatabaseContext()
            : base(ApplicationSettingsFactory.GetApplicationSettings().SqlConnectionString)
        {
            Initialize();
        }

        public ReadOnlyDatabaseContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {
            Initialize();
        }

        private void Initialize()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

            Database.SetInitializer<ReadOnlyDatabaseContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            if (!DataProviderConfigurator.IsConfigured) return;

            Array.ForEach(DataProviderConfigurator.MappingAssemblies.ToArray(), modelBuilder.AddMappingsFromAssemblyOf);
            Array.ForEach(DataProviderConfigurator.MappingAssemblies.ToArray(), modelBuilder.AddRegisterationFromAssemblyOf);
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            var query = base.Set<T>();
            //var objectQuery = query as System.Data.Objects.ObjectQuery;
            //if (objectQuery != null)
            //{
            //    var s = objectQuery.ToTraceString();
            //}
            return query;

        }
        public override int SaveChanges()
        {
            throw new NotSupportedException("Database is in readonly mode...");
        }
    }
}
