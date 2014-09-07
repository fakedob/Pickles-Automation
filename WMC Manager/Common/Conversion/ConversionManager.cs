using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Web;

namespace Common
{
    public class ConversionManager
    {
        public static Uri FormatUrlToScheme(Uri uri, bool secure)
        {
            Uri result = uri;
            string scheme = ValidationManager.IIF(secure, Uri.UriSchemeHttps, Uri.UriSchemeHttp).ToString();
            if (!string.Equals(uri.Scheme, scheme, StringComparison.OrdinalIgnoreCase))
            {
                result = new Uri(uri.ToString().Replace(uri.Scheme, scheme));
            }
            return result;
        }
        public static string ConvertYesNo(bool code_)
        {
            string desc_ = "";
            switch (code_)
            {
                case true:
                    desc_ = "Yes";
                    break;
                case false:
                    desc_ = "No";
                    break;
                default:
                    desc_ = "Unknown";
                    break;
            }
            return desc_;
        }
        public static string ConvertIncludeExclude(bool code_)
        {
            string desc_ = "";
            switch (code_)
            {
                case true:
                    desc_ = "Include";
                    break;
                case false:
                    desc_ = "Exclude";
                    break;
                default:
                    desc_ = "Unknown";
                    break;
            }
            return desc_;
        }
        public static string ConvertMobile(string mobile)
        {
            if (!ValidationManager.IsNullOrEmpty(mobile) && mobile.Length >= 10 && mobile.Length <= 12)
            {
                mobile = mobile.Replace(" ", "");
                if (mobile.Substring(0, 1) == "7" && mobile.Length == 10)
                {
                    mobile = string.Format("44{0}", mobile);
                }
                if (mobile.Substring(0, 2) == "07" && mobile.Length == 11)
                {
                    mobile = mobile.Remove(0, 1);
                    mobile = string.Format("44{0}", mobile);
                }
            }
            return mobile;
        }
        public static DataSet ToDataSet(Object obj)
        {
            DataSet ds = new DataSet();
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, obj);
            StringReader reader = new StringReader(writer.ToString());
            ds.ReadXml(reader);
            return ds;
        }
        public static double BytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        public static double KilobytesToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }
        public static string SerializeObject(object obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            try
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                doc.Load(stream);
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return doc.InnerXml;
        }
        public static object DeserializeObject(string serialised, Type T)
        {
            XmlSerializer xs = new XmlSerializer(T);
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(serialised));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }
        public static byte[] StringToUTF8ByteArray(string value)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(value);
            return byteArray;
        }
        public static byte[] ConvertObjectToByteArray(object obj)
        {
            MemoryStream ms = new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, obj);
            ms.Position = 0;
            byte[] byte_array = new byte[ms.Length];
            ms.Read(byte_array, 0, (int)ms.Length);
            ms.Close();
            return byte_array;
        }
        public static object ConvertByteArrayToObject(byte[] byte_array)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms.Write(byte_array, 0, byte_array.Length);
            ms.Position = 0;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return bf.Deserialize(ms);
        }

        public static double GetDateDiffMinutes(DateTime start_date, DateTime end_date)
        {
            TimeSpan ts = end_date - start_date;
            return ts.TotalMinutes;
        }
        public static int GetDateDiff(DateTime start_date, DateTime end_date)
        {
            return (end_date - start_date).Days;
        }
        public static int GetDateDiffMonths(DateTime start_date, DateTime end_date)
        {
            return Math.Abs(GetDateDiffMonthsActual(start_date, end_date));
        }
        public static int GetDateDiffMonthsActual(DateTime start_date, DateTime end_date)
        {
            int monthsApart = 12 * (start_date.Year - end_date.Year) + start_date.Month - end_date.Month;
            return monthsApart;
        }
        public static DateTime GetWeekStart(DateTime curr_date)
        {
            DateTime week_start;
            if (curr_date.DayOfWeek == DayOfWeek.Sunday)
            {
                week_start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            }
            else
            {
                week_start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            }
            return week_start;
        }

        public static DateTime GetFirstDayOfWeek(DateTime sourceDateTime, bool start_monday = true)
        {
            int date_diff = 0;
            if (start_monday)
            {
                date_diff = 1;
            }
            DayOfWeek daysAhead = (DayOfWeek.Sunday - (int)sourceDateTime.DayOfWeek);

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead + date_diff);

            return sourceDateTime;
        }

        public static DateTime GetLastDayOfWeek(DateTime sourceDateTime, bool end_saturday = true)
        {
            int date_diff = 0;
            if (end_saturday)
            {
                date_diff = 1;
            }
            DayOfWeek daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead + date_diff);

            return sourceDateTime;
        }

        public static DateTime GetMonthStart(DateTime curr_date)
        {
            return new DateTime(curr_date.Year, curr_date.Month, 1);
        }
        public static DateTime GetMonthEnd(DateTime curr_date)
        {
            return GetMonthStart(curr_date).AddMonths(1).AddDays(-1);
        }
        public static string TimeSpanToString(TimeSpan ts)
        {
            string result = string.Empty;
            if (ts.Days > 0)
            {
                result = string.Format("{0} days {1} hrs {2} mins", ts.Days, ts.Hours, ts.Minutes);
            }
            else
            {
                if (ts.Hours > 0)
                {
                    result = string.Format("{0} mins", ts.Minutes);
                }
                else
                {
                    result = string.Format("{0} hrs {1} mins", ts.Hours, ts.Minutes);
                }
            }
            return result;
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();
            return dtDateTime;
        }
        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            //Input parameters format - DateTime.Parse("2011-08-11 16:59"), TimeSpan.FromMinutes(15);
            return new DateTime(((dt.Ticks + d.Ticks / 2) / d.Ticks) * d.Ticks);
        }
        public static double DateTimeToUnixTimeStamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = new TimeSpan();
            span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        public static string GetWebResponse(WebResponse myHttpWebResponse)
        {
            StringBuilder rawResponse = new StringBuilder();
            try
            {
                using (Stream streamResponse = myHttpWebResponse.GetResponseStream())
                {
                    using (StreamReader streamRead = new StreamReader(streamResponse))
                    {
                        Char[] readBuffer = new Char[256];
                        int count = streamRead.Read(readBuffer, 0, 256);

                        while (count > 0)
                        {
                            String resultData = new String(readBuffer, 0, count);
                            rawResponse.Append(resultData);
                            count = streamRead.Read(readBuffer, 0, 256);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return rawResponse.ToString();
        }
        public static string ConvertStringArrayToString(string[] array, char separator)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(separator);
            }
            return builder.ToString();
        }
        public static string GetSql(string sp, SqlCommand cmd_obj)
        {
            string sql = "exec " + sp;

            for (int i = 0; i < (cmd_obj.Parameters.Count); i++)
            {
                sql += cmd_obj.Parameters[i].ParameterName + "=" + cmd_obj.Parameters[i].Value + ",";
            }

            return sql;
        }
        public static string UpperFirstLetter(string inValue)
        {
            if (inValue == null)
            {
                return string.Empty;
            }
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inValue.ToLower());
        }
        public static string GenerateRandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            int intElements = random.Next(10, 26);

            for (int i = 0; i < intElements; i++)
            {
                int intRandomType = random.Next(0, 2);
                if (intRandomType == 1)
                {
                    char ch;
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                else
                {
                    builder.Append(random.Next(0, 9));
                }
            }
            return builder.ToString();
        }
        public static string GetSubDomain(string url)
        {
            string subdomain = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                if (uri.HostNameType == UriHostNameType.Dns)
                {
                    string host = uri.Host;
                    if (host.Split('.').Length > 2)
                    {
                        int lastIndex = host.LastIndexOf(".");
                        int index = host.LastIndexOf(".", lastIndex - 4);
                        subdomain = host.Substring(0, index);
                    }
                }
            }
            catch { }
            return subdomain;
        }
        public static string ReplaceSubDomain(string url, string new_subdomain)
        {
            string new_url = string.Empty;
            Uri uri = new Uri(url);
            if (uri.HostNameType == UriHostNameType.Dns)
            {
                string host = uri.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 4);
                    string subdomain = host.Substring(0, index);
                    new_url = url.Replace(subdomain, new_subdomain);
                }
            }
            return new_url;
        }
        public static string GetObjectState(object obj, string property_name = null)
        {
            string state = string.Empty;
            PropertyInfo[] pi = obj.GetType().GetProperties();

            if (property_name == null)
            {
                for (int i = 0; i < pi.Length; i++)
                {
                    state = string.Format("{0}{1}:{2}<br>", state, pi[i].Name, pi[i].GetValue(obj, null));
                }
            }
            else
            {
                for (int i = 0; i < pi.Length; i++)
                {
                    if (pi[i].Name == property_name)
                        state = string.Format("{0}{1}:{2}<br>", state, pi[i].Name, pi[i].GetValue(obj, null));
                }
            }
            return state;
        }
        public static string TruncateString(string value, int length)
        {
            if (value == null || value.Trim().Length <= length)
                return value;

            int index = value.Trim().LastIndexOf(" ");

            while ((index + 3) > length) ;
            index = value.Substring(0, index).Trim().LastIndexOf(" ");

            if (index > 0)
                return value.Substring(0, index) + "...";

            return value.Substring(0, length - 3) + "...";
        }
        public static string TruncateStringWrap(string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }
        public static DateTime DateTimeUserNow(string timezone)
        {
            DateTime myDate = new DateTime();
            try
            {
                myDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timezone));
            }
            catch
            {
                myDate = DateTime.Now;
            }
            return myDate;
        }
        public static DateTimeOffset ConvertDateTimeToUser(DateTime date_time, string timezone)
        {
            TimeZoneInfo localInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return new DateTimeOffset(TimeZoneInfo.ConvertTimeFromUtc(date_time, localInfo), localInfo.BaseUtcOffset);
        }

        public static byte[] ushortArrayToByteArray(ushort[] myUshortArray)
        {
            byte[] myArray = new byte[myUshortArray.Length * 2];
            int count = 0;
            for (int i = 0; i < myUshortArray.Length; i++)
            {
                byte[] tempArray = BitConverter.GetBytes(myUshortArray[i]);
                myArray[count] = tempArray[0];
                myArray[count + 1] = tempArray[1];
                count += 2;
            }
            return myArray;
        }

        public static ushort[] byteArrayToUshortArray(byte[] myByteArray)
        {
            ushort[] myArray = new ushort[myByteArray.Length / 2];

            int count = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = BitConverter.ToUInt16(myByteArray, count);
                count += 2;
            }
            return myArray;
        }

    }
}