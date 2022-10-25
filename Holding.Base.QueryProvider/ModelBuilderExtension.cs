using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Holding.Base.QueryProvider
{
    internal static class ModelBuilderExtension
    {
        internal static void AddMappingsFromAssemblyOf( this DbModelBuilder modelBuilder , Assembly assembly )
        {
            Array.ForEach( assembly.GetTypes().Where( type => type.BaseType != null && type.BaseType.IsGenericType && ( type.BaseType.GetGenericTypeDefinition() == typeof( EntityTypeConfiguration<> ) || type.BaseType.GetGenericTypeDefinition() == typeof( ComplexTypeConfiguration<> ) ) ).ToArray() , delegate( Type type )
            {
                dynamic instance = Activator.CreateInstance( type );
                modelBuilder.Configurations.Add( instance );
            } );
        }

        internal static void AddRegisterationFromAssemblyOf( this DbModelBuilder modelBuilder , Assembly assembly )
        {
            Array.ForEach( assembly.GetTypes().Where( type => type.GetCustomAttributes( typeof( TableAttribute ) ).Any() ).ToArray() , modelBuilder.RegisterEntityType );
        }
    }
}
