using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.MongoMigration.Integrity
{
    public class CollectionTypesLocator : ICollectionTypesLocator
    {
        private readonly Assembly _repositoryAssembly;

        public CollectionTypesLocator(Assembly repositoryAssembly)
        {
            _repositoryAssembly = repositoryAssembly;
        }

        public List<Type> GetCollectionTypes()
        {
            var collectionTypes = _repositoryAssembly.GetTypes()
                .Where(i =>
                    i.GetInterfaces().Any(j =>
                        j.IsGenericType && j.GetGenericTypeDefinition() == typeof(IRepository<,>)))
                .Select(i =>
                    i.GetInterfaces().First(j =>
                        j.IsGenericType && j.GetGenericTypeDefinition() == typeof(IRepository<,>))
                    .GetGenericArguments().First())
                .ToList();

            return collectionTypes;
        }

        public List<Type> GetRepositoryTypes()
        {
            var repositoryTypes = _repositoryAssembly.GetTypes()
                .Where(i =>
                    i.GetInterfaces().Any(j =>
                        j.IsGenericType && j.GetGenericTypeDefinition() == typeof(IRepository<,>)))
                .ToList();

            return repositoryTypes;
        }
    }
}
