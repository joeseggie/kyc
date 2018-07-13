using System;
using System.Data.SqlClient;

namespace UgandaTelecom.Kyc.Core.Common.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static int SafeGetInt32(this SqlDataReader reader, int column, int defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetInt32(column);
            }
        }

        public static DateTime? SafeGetDateTime(this SqlDataReader reader, int column, DateTime? defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetDateTime(column);
            }
        }

        public static TimeSpan SafeGetTimeSpan(this SqlDataReader reader, int column, TimeSpan defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetTimeSpan(column);
            }
        }

        public static string SafeGetString(this SqlDataReader reader, int column, string defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetString(column);
            }
        }

        public static bool SafeGetBoolean(this SqlDataReader reader, int column, bool defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetBoolean(column);
            }
        }

        public static Guid? SafeGetGuid(this SqlDataReader reader, int column, Guid? defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetGuid(column);
            }
        }

        public static TimeSpan? SafeGetTimeSpan(this SqlDataReader reader, int column, TimeSpan? defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetTimeSpan(column);
            }
        }
    }
}
