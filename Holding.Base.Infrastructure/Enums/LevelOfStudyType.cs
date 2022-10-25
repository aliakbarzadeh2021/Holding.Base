using System.ComponentModel;

namespace Holding.Base.Infrastructure.Enums
{
    /// <summary>
    /// مقاطع تحصیلی
    /// </summary>
    public enum LevelOfStudyType
    {
        /// <summary>
        /// پیش دبستانی
        /// </summary>
        [Description("پیش دبستانی")]
        PreSchool = 0,
        /// <summary>
        /// دبستان
        /// </summary>
        [Description("ابتدایی")]
        PrimarySchool = 1,
        /// <summary>
        /// دوره اول متوسطه
        /// </summary>
        [Description("دوره اول متوسطه")]
        MiddleSchool = 2,
        /// <summary>
        /// دوره دوم متوسطه
        /// </summary>
        [Description("دوره دوم متوسطه")]
        HighSchool = 3
    }
}
