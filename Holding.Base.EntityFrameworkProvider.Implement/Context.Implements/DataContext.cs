using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;
using Holding.Base.EntityFrameworkProvider.Implement.Context.Initializers;
using Holding.Base.EntityFrameworkProvider.Implement.Helpers;
using Holding.Base.EntityFrameworkProvider.Read.Context.Interfaces;
using Holding.Base.EntityFrameworkProvider.Write.Context.Interfaces;
using Holding.Base.EntityFrameworkProvider.Write.Exceptions;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.EntityFrameworkProvider.Implement.Context.Implements
{
    internal class DataContext<TDataTable> : DbContext, IReadOnlyContext<TDataTable>, IDataContext<TDataTable> where TDataTable : DataTableBase
    {

        public DataContext() : base(ApplicationSettingsFactory.GetApplicationSettings().SqlConnectionString)
        {
            Initialize();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();


            //DataProviderConfigurator.AddMappingFromAssemblyOf<DataTableBase>();
            if (!DataProviderConfigurator.IsConfigured)
                throw new DataProviderAssembliesNotFoundException();

            //Register FluentMappings
            Array.ForEach(DataProviderConfigurator.MappingAssemblies.ToArray(), modelBuilder.AddMappingsFromAssemblyOf);

            //Register Entities
            var assemblies = DataProviderConfigurator.MappingAssemblies.ToArray();
            foreach (var assembly in assemblies)
            {
                modelBuilder.AddRegisterationFromAssemblyOf(assembly, this);
            }
            
        }



        private void Initialize()
        {

            if (ApplicationSettingsFactory.GetApplicationSettings().IsReadOnlyDataBase)
            {
                //IReadOnlyContext
                Configuration.LazyLoadingEnabled = true;
                Configuration.ProxyCreationEnabled = false;
                Configuration.ValidateOnSaveEnabled = false;
                Configuration.AutoDetectChangesEnabled = false;
            }
            else
            {
                //IDataContext
                Configuration.LazyLoadingEnabled = true;
                Configuration.ProxyCreationEnabled = true;
                Configuration.ValidateOnSaveEnabled = true;
                Configuration.AutoDetectChangesEnabled = true;
                Database.CommandTimeout = 1000;
            }
            
            //Database.SetInitializer(new DropCreateDatabaseAlways<DataContext<TDataTable>>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext<TDataTable>, Migrations.Configuration<TDataTable>>());
            CompositeDatabaseInitializer<DataContext<TDataTable>> compositeDatabaseInitializer =new CompositeDatabaseInitializer<DataContext<TDataTable>> (new MigrateDatabaseToLatestVersion<DataContext<TDataTable>, Migrations.Configuration<TDataTable>>(),new IndexInitializer<DataContext<TDataTable>>());
            Database.SetInitializer(compositeDatabaseInitializer);

        }



        #region IReadOnlyContext

        Read.Context.Interfaces.ISet<TEntity> IReadOnlyContext<TDataTable>.Set<TEntity>()
        {
            return new Set<TEntity>(base.Set<TEntity>());
        }

        
        public IQueryable<TDto> SqlQuery<TDto>(string storedProcedure,Dictionary<string,object> parameteres) where TDto : class
        {
            if (parameteres!=null && parameteres.Count>0)
            {
                object[] sqlParameters = new SqlParameter[parameteres.Count];
                int index = 0;
                foreach (var parametere in parameteres)
                {
                    SqlParameter sqlParameter = new SqlParameter(parametere.Key, parametere.Value);
                    sqlParameters[index] = sqlParameter;
                    index++;
                }
                return Database.SqlQuery<TDto>(storedProcedure, sqlParameters).AsQueryable();
            }

            return Database.SqlQuery<TDto>(storedProcedure).AsQueryable();
        }

        #endregion
        

        #region IDataContext

        Write.Context.Interfaces.ISet<TEntity> IDataContext<TDataTable>.Set<TEntity>()
        {
            return new Set<TEntity>(base.Set<TEntity>());
        }
        
        #endregion


    }
}
