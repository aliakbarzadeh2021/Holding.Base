using System.ComponentModel;

namespace Holding.Base.Bus.Response
{
    public enum AlarmType
    {
        [Description("����")]
        Success,
        [Description("������")]
        Error
    }
}