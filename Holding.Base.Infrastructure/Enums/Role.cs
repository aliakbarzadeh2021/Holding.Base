using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Role")]
    public enum UserRole
    {
        [Description("دانش آموز")]
        Student,

        [Description("معلم")]
        Teacher,

        [Description("مشاور")]
        Consultant,

        [Description("والدین")]
        Parent,

        [Description("کارمند")]
        Personnel
    }
}