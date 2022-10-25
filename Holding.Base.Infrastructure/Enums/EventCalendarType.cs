using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "EventCalendarType")]
    public enum EventCalendarType
    {
        [Description("مناسبت عادی")]
        RegularEvent = 1,
        [Description("جشن های ملی")]
        NationalCelebrations = 2,
        [Description("جشن های مذهبی")]
        ReligiousCelebrations = 3,
        [Description("سوگواری ملی")]
        NationalMourning = 4,
        [Description("سوگواری مذهبی")]
        ReligiousMourning = 5
    }
}
