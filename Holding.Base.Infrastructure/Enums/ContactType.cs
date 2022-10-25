using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "ContactType")]
    public enum ContactType
    {
        [Description("تلفنی")]
        ByPhone = 1,
        [Description("حضوری")]
        ByPresence = 2
    }
}
