using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Holding.Base.Infrastructure.Configuration
{
    public class DataProviderConfigurator
    {
        private static readonly List<Assembly> Assemblies;
        private static bool ftsIsEnabled;
        static DataProviderConfigurator()
        {
            Assemblies = new List<Assembly>();
        }

        public static bool IsFtsEnabled
        {
            get { return ftsIsEnabled; }
        }

        public static ReadOnlyCollection<Assembly> MappingAssemblies
        {
            get { return Assemblies.AsReadOnly(); }
        }

        public static void AddMappingFromAssemblyOf<T>()
        {
            Assemblies.Add( typeof( T ).Assembly );
        }

        public static void AddMappingFromAssemblyOf( Type type )
        {
            Assemblies.Add( type.Assembly );
        }

        public static bool IsConfigured
        {
            get { return Assemblies.Any(); }
        }

        public static void EnableFullTextSearch()
        {
            ftsIsEnabled = true;
        }

        public static void DisableFullTextSearch()
        {
            ftsIsEnabled = false;
        }
    }
}
