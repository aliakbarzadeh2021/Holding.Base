using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Holding.Base.Infrastructure.Configuration
{
    public class StoredProcedureConfigurator
    {
        private static readonly List<Assembly> Assemblies;
        static StoredProcedureConfigurator()
        {
            Assemblies = new List<Assembly>();
        }
        

        public static ReadOnlyCollection<Assembly> MappingAssemblies
        {
            get { return Assemblies.AsReadOnly(); }
        }

        public static void AddMappingFromAssemblyOf<T>()
        {
            Assemblies.Add(typeof(T).Assembly);
        }

        public static void AddMappingFromAssemblyOf(Type type)
        {
            Assemblies.Add(type.Assembly);
        }

        public static bool IsConfigured
        {
            get { return Assemblies.Any(); }
        }

        
    }
}