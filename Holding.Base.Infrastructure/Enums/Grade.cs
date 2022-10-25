using System.ComponentModel;

namespace Holding.Base.Infrastructure.Enums
{
    /// <summary>
    /// پایه تحصیلی
    /// </summary>
    public enum Grade
    {
        /// <summary>
        /// بدون پایه
        /// </summary>
        [Description("پیش دبستانی")]
        Prep = 0,
        /// <summary>
        /// پایه اول
        /// </summary>
        [Description("پایه اول")]
        First = 1,
        /// <summary>
        /// پایه دوم
        /// </summary>
        [Description("پایه دوم")]
        Second = 2,
        /// <summary>
        /// پایه سوم
        /// </summary>
        [Description("پایه سوم")]
        Third = 3,
        /// <summary>
        /// پایه چهارم
        /// </summary>
        [Description("پایه چهارم")]
        Fourth = 4,
        /// <summary>
        /// پایه پنجم
        /// </summary>
        [Description("پایه پنجم")]
        Fifth = 5,
        /// <summary>
        /// پایه ششم
        /// </summary>
        [Description("پایه ششم")]
        Sixth = 6,
        /// <summary>
        /// پایه هفتم
        /// </summary>
        [Description("پایه هفتم")]
        Seventh = 7,
        /// <summary>
        /// پایه هشتم
        /// </summary>
        [Description("پایه هشتم")]
        Eighth = 8,
        /// <summary>
        /// پایه نهم
        /// </summary>
        [Description("پایه نهم")]
        Ninth = 9,
        /// <summary>
        /// پایه دهم
        /// </summary>
        [Description("پایه دهم")]
        Tenth = 10,
        /// <summary>
        /// پایه یازدهم
        /// </summary>
        [Description("پایه یازدهم")]
        Eleventh = 11,
        /// <summary>
        /// پایه دوازدهم
        /// </summary>
        [Description("پایه دوازدهم")]
        Twelfth = 12
    }
}