using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Role")]
    public enum UserRole
    {
        [Description("دانش آموز")]
        Student = 0,

        [Description("معلم")]
        Teacher = 1,

        [Description("مشاور")]
        Consultant = 2,

        [Description("والدین")]
        Parent = 3,

        [Description("کارمند")]
        Personnel = 4,

        [Description("ادمین مدرسه")]
        SchoolAdmin = 5,

        [Description("پشتیبان")]
        Supporter = 6,

        [Description("کارمند مجتمع")]
        ComplexPersonnel = 7,

        [Description("ادمین مجتمع")]
        ComplexAdmin = 8

    }
}