using System;

namespace Holding.Base.EntityFrameworkProvider.Read.Attributes
{
    public class SpParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public string SqlType { get; set; }
    }
}