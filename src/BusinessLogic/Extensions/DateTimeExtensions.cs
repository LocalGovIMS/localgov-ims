using System;

namespace BusinessLogic.Extensions
{
    public static class DateTimeExtensions
    { 
        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }
    }
}
