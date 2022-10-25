using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "WeeklyScheduleType")]
    public enum WeeklyScheduleType
    {
        [Description("تمام هفته ها")]
        AllWeeks = 1,
        [Description("هفته های زوج")]
        EvenWeeks = 2,
        [Description("هفته های فرد")]
        OddWeeks = 3
    }
}
