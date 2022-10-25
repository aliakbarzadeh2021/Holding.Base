using System.ComponentModel;

namespace Holding.Base.Infrastructure.Enums
{
    public enum HighSchoolType
    {
        /// <summary>
        /// دبیرستان نظری
        /// </summary>
        [Description("دبیرستان نظری")]
        Default = 1,
        /// <summary>
        /// دبیرستان نظری
        /// </summary>
        [Description("هنرستان کارودانش")]
        VocationalSchool = 2,
        /// <summary>
        /// هنرستان فنی و حرفه ای
        /// </summary>
        [Description("هنرستان فنی و حرفه ای")]
        TechnicalSchool = 3
    }
}