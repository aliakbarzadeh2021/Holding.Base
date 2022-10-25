
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.EntityFrameworkProvider.Implement.Context.Initializers
{
    public class IndexInitializer<T> : IDatabaseInitializer<T> where T : DbContext
    {

        

        //private const string CreateIndexQueryTemplate = "CREATE {unique} INDEX {indexName} ON {tableName} ({columnName})";

        private const string CreateIndexQueryTemplate =
            "IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = '{indexName}' AND object_id = OBJECT_ID('{tableName}')) " +
            "BEGIN " +
            "CREATE {unique} INDEX {indexName} ON {tableName} ({columnName}) " +
            "END";

        public void InitializeDatabase(T context)
        {
            const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

            var assemblies = DataProviderConfigurator.MappingAssemblies.ToArray();
            List<Type> entityTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                entityTypes.AddRange(
                assembly.GetTypes()
                    .Where(
                        type =>
                            !type.IsAbstract &&
                            type.BaseType == context.GetType().GenericTypeArguments[0]
                            )
                    .ToArray());
            }


            foreach (var entityType in entityTypes)
            {
                TableAttribute[] tableAttributes = (TableAttribute[])entityType.GetCustomAttributes(typeof(TableAttribute), false);
                foreach (var property in entityType.GetProperties(PublicInstance))
                {
                    Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute[] indexAttributes = (Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute[])property.GetCustomAttributes(typeof(Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute), false);
                    NotMappedAttribute[] notMappedAttributes = (NotMappedAttribute[])property.GetCustomAttributes(typeof(NotMappedAttribute), false);
                    if (indexAttributes.Length > 0 && notMappedAttributes.Length == 0)
                    {
                        ColumnAttribute[] columnAttributes = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), false);

                        foreach (var indexAttribute in indexAttributes)
                        {
                            string indexName = indexAttribute.Name;
                            string tableName = tableAttributes.Length != 0 ? tableAttributes[0].Name : entityType.Name;
                            string columnName = columnAttributes.Length != 0 ? columnAttributes[0].Name : property.Name;
                            string query = CreateIndexQueryTemplate.Replace("{indexName}", indexName)
                                .Replace("{tableName}", tableName)
                                .Replace("{columnName}", columnName)
                                .Replace("{unique}", indexAttribute.IsUnique ? "UNIQUE" : string.Empty);

                            context.Database.CreateIfNotExists();

                            context.Database.ExecuteSqlCommand(query);
                        }
                    }
                }
            }



/*

            foreach (var dataSetProperty in typeof(T).GetProperties(PublicInstance).Where(p => p.PropertyType.Name == typeof(DbSet<>).Name))
            {
                var entityType = dataSetProperty.PropertyType.GetGenericArguments().Single();

                TableAttribute[] tableAttributes = (TableAttribute[])entityType.GetCustomAttributes(typeof(TableAttribute), false);

                foreach (var property in entityType.GetProperties(PublicInstance))
                {
                    Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute[] indexAttributes = (Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute[])property.GetCustomAttributes(typeof(Holding.Base.EntityFrameworkProvider.Attributes.IndexAttribute), false);
                    NotMappedAttribute[] notMappedAttributes = (NotMappedAttribute[])property.GetCustomAttributes(typeof(NotMappedAttribute), false);
                    if (indexAttributes.Length > 0 && notMappedAttributes.Length == 0)
                    {
                        ColumnAttribute[] columnAttributes = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), false);

                        foreach (var indexAttribute in indexAttributes)
                        {
                            string indexName = indexAttribute.Name;
                            string tableName = tableAttributes.Length != 0 ? tableAttributes[0].Name : dataSetProperty.Name;
                            string columnName = columnAttributes.Length != 0 ? columnAttributes[0].Name : property.Name;
                            string query = CreateIndexQueryTemplate.Replace("{indexName}", indexName)
                                .Replace("{tableName}", tableName)
                                .Replace("{columnName}", columnName)
                                .Replace("{unique}", indexAttribute.IsUnique ? "UNIQUE" : string.Empty);

                            context.Database.CreateIfNotExists();

                            context.Database.ExecuteSqlCommand(query);
                        }
                    }
                }
            }
*/



        }
    }
}