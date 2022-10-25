using System;
using System.Data.Entity.Migrations;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;
using Holding.Base.EntityFrameworkProvider.Implement.Context.Implements;

namespace Holding.Base.EntityFrameworkProvider.Implement.Migrations
{
    internal sealed class Configuration<TDataTable> : DbMigrationsConfiguration<DataContext<TDataTable>> where TDataTable : DataTableBase
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            //ContextKey = this.GetType().Namespace;
        }

        protected override void Seed(DataContext<TDataTable> context)
        {
            
        }
    }


    
}
