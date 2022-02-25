using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace UAP.Shared.Helper
{
    /// <summary>
    /// 日期时间 Helper
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取 CST 时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCstDateTime()
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();
            return now.InZone(TimeZone).ToDateTimeUnspecified();
        }

        /// <summary>
        /// 获取 CST 时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>

        public static DateTimeZone TimeZone = DateTimeZoneProviders.Tzdb["Asia/Shanghai"];
        public static DateTime ToCstTime(this DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
            {
                return Instant.FromDateTimeUtc(time).InZone(TimeZone).ToDateTimeUnspecified();
            }
            return time;
        }
    }
}
