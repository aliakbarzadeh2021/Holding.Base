using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "FamilyRelative")]
    public enum FamilyRelative
    {
        [Description("پدر")]
        Father = 1,
        [Description("مادر")]
        Mother = 2,
        [Description("برادر")]
        Brother = 3,
        [Description("خواهر")]
        Sister = 4,
        [Description("عمو")]
        Uncle = 5,
        [Description("عمه")]
        Aunt = 6,
        [Description("پدر بزرگ")]
        Grandfather = 7,
        [Description("مادر بزرگ")]
        Grandmother = 8
    }
}
