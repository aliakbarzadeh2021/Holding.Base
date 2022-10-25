using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "PersonnelType")]
    public enum PersonnelType
    {
        [Description("آموزگار")]
        Teacher = 1,
        [Description("کارمند")]
        Personnel = 2,
        [Description("مدیر")]
        Manager = 3,
        [Description("معاون")]
        Assistance = 4,
        [Description("پشتیبان")]
        ITManager = 5
    }
}