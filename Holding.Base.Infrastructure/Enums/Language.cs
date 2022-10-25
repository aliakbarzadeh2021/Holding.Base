
using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Language")]
    public enum Language
    {
        [Description("فارسی")]
        Persian = 1,
        [Description("انگلیسی")]
        English = 2,
        [Description("عربی")]
        Arabic = 3
    }
}