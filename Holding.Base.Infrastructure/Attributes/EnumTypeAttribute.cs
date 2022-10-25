using System;

namespace Holding.Base.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class EnumTypeAttribute : Attribute
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
