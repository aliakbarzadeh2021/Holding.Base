using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    /// <summary>
    /// مقطع آموزشی
    /// </summary>
    [EnumType(Name = "LevelOfEducation")]
    public enum LevelOfEducation
    {
        [Description("تحصیلات متوسطه یا زیر دیپلم")]
        HighSchool = 1,
        [Description("دیپلم")]
        Deploma = 2,
        [Description("کاردانی")]
        Technician = 3,
        [Description("کارشناسی")]
        BSc = 4,
        [Description("کارشناسی ارشد")]
        MSc = 5,
        [Description("دکتری")]
        PhD = 6

    }
}
