using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    /// <summary>
    /// جنسیت
    /// </summary>
    [EnumType(Name = "SchoolGender")]
    public enum SchoolGender
    {
        [Description("پسرانه")]
        Male = 1,
        [Description("دخترانه")]
        Female = 2,
        [Description("مختلط")]
        Coeducational = 3
    }
}
