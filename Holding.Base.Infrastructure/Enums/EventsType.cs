using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "EventsType")]
    public enum EventsType
    {
        [Description("مناسبت عادی")]
        RegularEvents = 1,

        [Description("جشن ها و اعیاد ملی")]
        NationalCelebrations = 2,

        [Description("سوگواری ملی")]
        NationalMourning = 3,

        [Description("جشن ها و اعیاد مذهبی")]
        ReligiousCelebrations = 4,

        [Description("سوگواری مذهبی")]
        ReligiousMourning = 5
    }
}