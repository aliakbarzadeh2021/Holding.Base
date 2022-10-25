using System.Linq;
using System.Reflection;
using Holding.Base.EntityFrameworkProvider.Read.Attributes;
using Holding.Base.EntityFrameworkProvider.Read.Exceptions;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.EntityFrameworkProvider.Read.StoredProcedures.Scanner
{
    public static class StoredProcedureScanner
    {
        public static void ScanAndBulid()
        {

            if (!StoredProcedureConfigurator.IsConfigured)
            {
                throw new StoredProcedureAssembliesNotFoundException();
            }

            var lst =
                StoredProcedureConfigurator.MappingAssemblies.First().GetTypes().Where(x => x.IsClass && x.CustomAttributes.Any(c => c.AttributeType == typeof(StoredProcedureAttribute)));
            foreach (var @class in lst)
                @class.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
        }
    }
}
