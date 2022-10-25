using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Degree")]
    public enum Degree
    {
        [Description("ابتدایی")]
        Elementary = 1,
        [Description("راهنمایی")]
        FirstPeriodHighSchool = 2,
        [Description("دبیرستان")]
        SecondPeriodHighSchool = 3
    }
}
