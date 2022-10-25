using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "RelationshipType")]
    public enum RelationshipType
    {
        [Description("پدر")]
        Father = 1,
        [Description("مادر")]
        Mother = 2,
        [Description("سایر")]
        Others = 3,
        [Description("سرپرست یا قیم")]
        GodFather = 4
    }
}
