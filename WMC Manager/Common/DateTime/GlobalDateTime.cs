using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicklesAutomation
{
    public class GlobalDateTime
    {
        public static DateTime ToLocalDateTime(DateTime utc_time, TimeZoneInfo time_zone_info)
        {
            return TimeZoneInfo.ConvertTime(utc_time, time_zone_info);
        }
        //public DateTime DateTimeNow()
        //{
        //    return TimeZoneInfo.ConvertTime(DateTime.UtcNow, myUser.Globalization.TimeZoneInfo);
        //}
    }
    //public struct GlobalDateTime
    //{
    //    private readonly DateTime utcDateTime;
    //    private readonly TimeZoneInfo timeZone;

    //    public GlobalDateTime(DateTime dateTime, TimeZoneInfo timeZone)
    //    {
    //        utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
    //        this.timeZone = timeZone;
    //    }

    //    public DateTime UTCDateTime { get { return utcDateTime; } }

    //    public TimeZoneInfo TimeZoneInfo { get { return timeZone; } }

    //    public DateTime LocalDateTime
    //    {
    //        get
    //        {
    //            return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
    //        }
    //    }
    //}
}
