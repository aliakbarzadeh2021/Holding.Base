using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "EmploymentType")]
    public enum EmploymentType
    {
        [Description("استخدامی")]
        Employed = 1,
        [Description("بازنشته")]
        Retired = 2,
        [Description("حق التدریس")]
        Tuition = 3,
        [Description("سایر")]
        Other = 4
    }
}
