using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "EducationsType")]
    public enum EducationsType
    {
        [Description("دبیرستان")]
        HighSchool = 1,
        [Description("دیپلم")]
        Diploma = 2,
        [Description("کاردانی")]
        AssociateDegree = 3,
        [Description("کارشناسی")]
        License = 4,
        [Description("کارشناسی ارشد")]
        Masters = 5,
        [Description("دکتری")]
        Phd = 6
        
    }
}