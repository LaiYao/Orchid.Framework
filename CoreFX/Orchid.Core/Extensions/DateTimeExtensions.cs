using System;

namespace Orchid.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 是否周末
        /// <summary>
        public static bool IsWeekend(this DateTime date)
        => date.DayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);

        /// <summary>
        /// 是否工作日
        /// <summary>
        public static bool IsWeekDay(this DateTime date)
        => !date.IsWeekend();
    }
}
