using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{

    [EnumType(Name = "ExamType")]
    public enum ExamType
    {

        /// <summary>
        /// تشریحی- گزینه ای
        /// </summary>
        [Description("تشریحی- گزینه ای")]
        DescriptiveMultipleChoice = 1,


        /// <summary>
        /// گزینه ای
        /// </summary>
        [Description("گزینه ای")]
        MultipleChoice = 2,


        /// <summary>
        /// تشریحی
        /// </summary>
        [Description("تشریحی")]
        Descriptive = 3,


    }
    
}