using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    /// <summary>
    /// وضعیت نظام وظیفه
    /// </summary>
    [EnumType(Name = "MilitaryStatus")]
    public enum MilitaryStatus
    {
        [Description("نامشخص")]
        None = 0,
        [Description("معافیت")]
        Exemption = 1,
        [Description("انجام داده")]
        Done = 2
    }
}
