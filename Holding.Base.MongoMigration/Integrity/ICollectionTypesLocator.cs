using System;
using System.Collections.Generic;

namespace Holding.Base.MongoMigration.Integrity
{
    internal interface ICollectionTypesLocator
    {
        List<Type> GetCollectionTypes();
        List<Type> GetRepositoryTypes();
    }
}
