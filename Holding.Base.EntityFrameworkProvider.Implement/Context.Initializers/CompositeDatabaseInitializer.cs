using System.Collections.Generic;
using System.Data.Entity;

namespace Holding.Base.EntityFrameworkProvider.Implement.Context.Initializers
{
    public class CompositeDatabaseInitializer<T> : IDatabaseInitializer<T> where T : DbContext
    {
        private readonly List<IDatabaseInitializer<T>> initializers;

        public CompositeDatabaseInitializer(params IDatabaseInitializer<T>[] databaseInitializers)
        {
            this.initializers = new List<IDatabaseInitializer<T>>();
            this.initializers.AddRange(databaseInitializers);
        }

        public void InitializeDatabase(T context)
        {
            foreach (var databaseInitializer in this.initializers)
            {
                databaseInitializer.InitializeDatabase(context);
            }
        }

        public void AddInitializer(IDatabaseInitializer<T> databaseInitializer)
        {
            this.initializers.Add(databaseInitializer);
        }

        public void RemoveInitializer(IDatabaseInitializer<T> databaseInitializer)
        {
            this.initializers.Remove(databaseInitializer);
        }
    }
}