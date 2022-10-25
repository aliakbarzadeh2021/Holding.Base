using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "AbsenceType")]
    public enum AbsenceType
    {
        [Description("روزانه")]
        Daily = 1 ,
        [Description("ساعتی")]
        Hourly = 2
    }
} 
