using System;

namespace Holding.Base.EntityFrameworkProvider.Read.Attributes
{
    public class StoredProcedureAttribute : Attribute
    {
        public string Name { get; private set; }

        public StoredProcedureAttribute(string name)
        {
            Name = name;
        }
    }
}
