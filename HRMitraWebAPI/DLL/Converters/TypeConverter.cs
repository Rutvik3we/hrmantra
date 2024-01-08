using System;
using System.Data.SqlTypes;

namespace NConverters
{
    public static class TypeConverter
    {
        //public static object DateTimeNullCheck(DateTime? value)
        //{
        //    return !string.IsNullOrEmpty(value.ToString())
        //            ? string.Format("'{0}'", Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"))
        //            : (object)System.Data.SqlTypes.SqlDateTime.Null;
        //}

        //public static object DecimalNullCheck(decimal? value)
        //{
        //    return !string.IsNullOrEmpty(value.ToString()) ? Convert.ToDecimal(value) : (object)System.Data.SqlTypes.SqlDecimal.Null;
        //}

        //public static object IntNullCheck(int? value)
        //{
        //    return !string.IsNullOrEmpty(value.ToString()) ? Convert.ToInt32(value) : (object)System.Data.SqlTypes.SqlInt32.Null;
        //}

        public static SqlDateTime DateTimeTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : Convert.ToDateTime(value);
        }

        public static SqlDateTime? NullableDateTimeTryParse(object value)
        {
            return (DBNull.Value.Equals(value) || value == null) ? SqlDateTime.Null : Convert.ToDateTime(value);
        }

        public static decimal DecimalTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : Convert.ToDecimal(value);
        }

        public static decimal? NullableDecimalTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (decimal?)null : Convert.ToDecimal(value);
        }

        public static int IntegerTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : Convert.ToInt32(value);
        }

        public static int? NullableIntegerTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (int?)null : Convert.ToInt32(value);
        }

        public static long LongTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : Convert.ToInt64(value);
        }

        public static long? NullableLongTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (long?)null : Convert.ToInt64(value);
        }

        public static char CharTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : Convert.ToChar(value);
        }

        public static char? NullableCharTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (char?)null : Convert.ToChar(value);
        }

        public static bool BooleanTryParse(object value)
        {
            return !DBNull.Value.Equals(value) && Convert.ToBoolean(value);
        }

        public static bool? NullableBooleanTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (bool?)null : Convert.ToBoolean(value);
        }

        public static TimeSpan TimeSpanTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? default : TimeSpan.Parse(Convert.ToString(value));
        }

        public static TimeSpan? NullableTimeSpanTryParse(object value)
        {
            return DBNull.Value.Equals(value) ? (TimeSpan?)null : TimeSpan.Parse(Convert.ToString(value));
        }
    }
}
