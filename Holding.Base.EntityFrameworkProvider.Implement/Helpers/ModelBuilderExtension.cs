using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Holding.Base.EntityFrameworkProvider.Context.Contracts;
using Holding.Base.EntityFrameworkProvider.Implement.Context.Implements;

namespace Holding.Base.EntityFrameworkProvider.Implement.Helpers
{
    internal static class ModelBuilderExtension
    {
        public static void AddMappingsFromAssemblyOf(this DbModelBuilder modelBuilder, Assembly assembly)
        {
            Array.ForEach(assembly.GetTypes().Where(type => type.BaseType != null && type.BaseType.IsGenericType && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>) || type.BaseType.GetGenericTypeDefinition() == typeof(ComplexTypeConfiguration<>))).ToArray(), delegate (Type type)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            });
        }


        public static void AddRegisterationFromAssemblyOf<TDataTable>(this DbModelBuilder modelBuilder, Assembly assembly, DataContext<TDataTable> context) where TDataTable : DataTableBase
        {
            //var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            //Array.ForEach(assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(TableAttribute)).Any()).ToArray(), modelBuilder.RegisterEntityType);

            var entityTypes =
                assembly.GetTypes()
                    .Where(
                        type =>
                            !type.IsAbstract &&
                            type.BaseType == context.GetType().GenericTypeArguments[0]
                            )
                    .ToArray();
            
            /* foreach (var type in entityTypes)
             {
                 entityMethod.MakeGenericMethod(type)
                   .Invoke(modelBuilder, new object[] { });
             }*/

            Array.ForEach(entityTypes, modelBuilder.RegisterEntityType);
        }
    }
}
