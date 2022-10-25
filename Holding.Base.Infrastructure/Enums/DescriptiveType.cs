using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "DescriptiveType")]
    public enum DescriptiveType
    {
        [Description("خیلی خوب")]
        VeryGood = 1,
        [Description("خوب")]
        Good = 2,
        [Description("قابل قبول")]
        Acceptable = 3,
        [Description("نیاز به توجه بیشتر")]
        MoreEffortNeeded = 4
    }
}
