using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "MaritalStatus")]
    public enum MaritalStatus
    {
        [Description("مجرد")]
        Single = 1,
        [Description("متاهل")]
        Married = 2,
        [Description("مطلقه")]
        Divorced = 3
    }
}
