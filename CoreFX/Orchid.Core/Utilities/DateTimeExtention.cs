using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Core.Utilities
{
    public static class DateTimeExtention
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
