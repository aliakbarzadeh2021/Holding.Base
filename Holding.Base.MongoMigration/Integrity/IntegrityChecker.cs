using System;
using System.Linq;
using System.Reflection;
using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.MongoMigration.Integrity
{
    public class MongoIntegrityChecker
    {
        private readonly CollectionTypesLocator _locator;

        public MongoIntegrityChecker(Assembly repositoryAssembly)
        {
            _locator = new CollectionTypesLocator(repositoryAssembly);
        }

        public void Start()
        {
            var repositories = _locator.GetRepositoryTypes();

            repositories.ForEach(repo =>
            {
                Console.Write($"Integrity Checking: \"{repo.Name}\"...");
                var ctor = repo.GetConstructors().OrderBy(c => c.GetParameters().Length).First();
                var parameters = Enumerable.Repeat((object)null, ctor.GetParameters().Length).ToArray();
                
                //var rx = Activator.CreateInstance(r, parameters) as as IIntegrityChecker;
                var rx = ctor.Invoke(parameters) as IIntegrityChecker;
                if (rx == null)
                    throw new NullReferenceException($"Cannot instantiate {repo.Name} repository.");
                rx.IntegrityCheck();
                Console.WriteLine(" completed.");
            });

        }
    }
}
