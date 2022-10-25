using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "SmsGroup")]
    public enum RecieverSmsGroupType
    {
        [Description("")]
        ForStudents = 0,
        ForParents= 1,
        ForTeachers = 2,
        ForManagers = 3,
        ForAssistants = 4,
        ForItManagers = 5,
        ForPersonels = 6
    }
}