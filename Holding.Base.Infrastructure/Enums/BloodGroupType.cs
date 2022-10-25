using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "BloodGroupType")]
    public enum BloodGroupType
    {
        [Description("A+")]
        APositive = 1,

        [Description("A-")]
        ANegative = 2,

        [Description("B+")]
        BPositive = 3,

        [Description("B-")]
        BNegative = 4,

        [Description("AB+")]
        ABPositive = 5,

        [Description("AB-")]
        ABNegative = 6,

        [Description("O+")]
        OPositive = 7,

        [Description("O-")]
        ONegative = 8
    }
}