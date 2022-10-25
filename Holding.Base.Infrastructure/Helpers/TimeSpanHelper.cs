using System;

namespace Holding.Base.Infrastructure.Helpers
{
    public static class TimeSpanHelper
    {


        /// <summary>
        /// مقدار بازه زمانی را بصورت ساعتی و دقیقه نمایش میدهد
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string HourDisplay(this TimeSpan src)
        {
            var totalMinute = src.Days * 24 * 60 + src.Hours * 60 + src.Minutes;

            var hourPart = totalMinute / 60;
            var minutePart = (totalMinute - hourPart * 60) % 60;

            return String.Format("{0}:{1:d2}", hourPart, minutePart);
        }
    }
}