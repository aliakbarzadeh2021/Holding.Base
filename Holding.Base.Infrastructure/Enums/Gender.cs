using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Gender")]
    public enum Gender
    {
        [Description("آقا")]
        Male = 1,
        [Description("خانم")]
        Female = 0
    }
}