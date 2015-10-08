using System;

namespace Orchid.Core.Utilities
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        public static bool IsWeekDay(this DateTime date)
        {
            return !date.IsWeekend();
        }
    }
}
