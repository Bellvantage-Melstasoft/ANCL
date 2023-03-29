using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Common
{
    public static class LocalTime
    {
        public static DateTime Now
        {
            get
            {
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time"));
            }
        }

        public static DateTime Today
        {
            get
            {
                return TimeZoneInfo.ConvertTime(DateTime.Today, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time"));
            }
        }
        public static DateTime ToLocalTime(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, TimeZoneInfo.Local.Id, "Sri Lanka Standard Time");
        }
    }
}
