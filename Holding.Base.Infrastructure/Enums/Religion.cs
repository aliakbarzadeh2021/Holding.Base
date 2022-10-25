using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Religion")]
    public enum Religion
    {
        [Description("اسلام-شیعه")]
        IslamShia = 1,
        [Description("اسلام-سنی")]
        IslamSunni = 2,
        [Description("مسیحیت")]
        Christianity = 3,
        [Description("یهودیت")]
        Judaism = 4,
        [Description("زرتشتی")]
        Zoroaster = 5
    }
}
