using System;
using System.Data;
using System.Globalization;

namespace DatabaseSchemaReader.ProviderSchemaReaders.Databases
{
    static class DataRecordExtensions
    {
        public static string GetString(this IDataRecord record, string fieldName)
        {
            // lann
            // 优化避免fieldName不存在时抛异常
            if (!TryGetOrdinal(record, fieldName, out var ordinal)) return null;
            var value = record[fieldName];
            // lann
            if (value == null || value == DBNull.Value) return null;
            return value.ToString();
        }

        // lann
        private static bool TryGetOrdinal(IDataRecord record, string fieldName, out int ordinal)
        {
            ordinal = -1;
            if (record == null) return false;
            if (string.IsNullOrEmpty(fieldName)) return false;

            // IDataRecord.GetOrdinal 在不存在列名时会抛 IndexOutOfRangeException
            try
            {
                ordinal = record.GetOrdinal(fieldName);
                return ordinal >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public static int? GetNullableInt(this IDataRecord record, string fieldName)
        {
            var value = record[fieldName];
            try
            {
                return (value != DBNull.Value) ? System.Convert.ToInt32(value, CultureInfo.CurrentCulture) : (int?)null;
            }
            catch (OverflowException)
            {
                //this occurs for blobs and clobs using the OleDb provider
                return -1;
            }
        }

        public static int GetInt(this IDataRecord record, string fieldName)
        {
            return GetNullableInt(record, fieldName).GetValueOrDefault();
        }

        public static long? GetNullableLong(this IDataRecord record, string fieldName)
        {
            var value = record[fieldName];
            try
            {
                return (value != DBNull.Value) ? System.Convert.ToInt64(value, CultureInfo.CurrentCulture) : (long?)null;
            }
            catch (OverflowException)
            {
                //this occurs for blobs and clobs using the OleDb provider
                return -1;
            }
        }

        public static bool GetBoolean(this IDataRecord record, string fieldName)
        {
            var value = record[fieldName];
            if (value is bool) //SqlLite has a true boolean
            {
                return (bool)value;
            }
            var s = value.ToString();
            if (s == "0") return false;
            if (s == "1") return true;
            if (s == "-1") return true;
            //could be Y, YES, N, NO, true, false.
            if (s.StartsWith("Y", StringComparison.OrdinalIgnoreCase) || s.StartsWith("T", StringComparison.OrdinalIgnoreCase)) //Y or YES
                return true;
            if (s.StartsWith("N", StringComparison.OrdinalIgnoreCase) || s.StartsWith("F", StringComparison.OrdinalIgnoreCase)) //N or NO
                return false;
            return false;
        }
    }
}
