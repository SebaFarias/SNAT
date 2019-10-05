using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Helper
{
    public static class TypeHelper
    {
        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }

        public static DateTime? ParseStringToDatetime(string value, string format)
        {
            if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }

        public static int? ConvertToIntNullable(object o)
        {
            if (o == null)
                return null;
            else
            {
                if (o.ToString() == "")
                    return null;
                return (int?)Convert.ToInt32(o);
            }
        }

        public static bool IsInteger(string o)
        {
            int x = 0;
            if (int.TryParse(o, out x))
                return true;
            else
                return false;
        }

        public static int ConvertToInt(object o)
        {
            if (o == null || o as string == "")
                return 0;
            else
                return Convert.ToInt32(o);
        }

        public static DateTime? ConvertToDateTimeNullable(object o)
        {
            if (o == null || o as string == "")
                return null;
            else
            {
                try
                {
                    DateTime d = Convert.ToDateTime(o);
                    return (DateTime?)d;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DateTime? ConvertToDateTimeNullable(object o, string format)
        {
            if (o == null || o as string == "")
                return null;
            else
            {
                try
                {
                    DateTime d = DateTime.ParseExact(o as string, format, CultureInfo.CurrentCulture);
                    return (DateTime?)d;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static decimal? ConvertToDecimalNullable(object o)
        {
            if (o == null || o as string == "")
            {
                return null;
            }
            else
            {
                return (decimal?)Convert.ToDecimal(o);
            }
        }

        public static decimal ConvertToDecimal(object o)
        {
            if (o == null || o as string == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(o);
            }
        }

        public static Dictionary<string, object> ResultDictionary(object o)
        {
            if (o == null)
                return null;
            else
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                PropertyInfo[] propiedades = o.GetType().GetProperties();
                foreach (PropertyInfo p in propiedades)
                {
                    item.Add(p.Name, (p.GetValue(o, null) == null) ? null : p.GetValue(o, null));
                }
                return item;
            }
        }

        public static string ConvertObjectToString(object o)
        {
            if (o == null)
                return null;

            if (o.ToString().Equals("null", StringComparison.InvariantCultureIgnoreCase))
                return null;
            else
                return o.ToString();
        }

        public static string ConvertDecimalToString(decimal? o)
        {
            decimal d = o ?? 0;
            string val = TypeHelper.ConvertObjectToString(d);
            return val.Replace(".", ",");
        }

        public static bool ConvertToBoolean(object o)
        {
            int num = 0;
            if (o != null)
            {
                if (int.TryParse(o as string, out num))
                {
                    return Convert.ToBoolean(Convert.ToInt32(num));
                }
                else
                {
                    return Convert.ToBoolean(o);
                }
            }
            else
                return false;
        }

        public static Dictionary<string, string> MergeDictionary(Dictionary<string, string> d1, Dictionary<string, string> d2)
        {
            if (d1 != null && d2 != null)
            {
                foreach (var item in d2)
                {
                    d1[item.Key] = item.Value;
                }
            }

            if (d1 == null && d2 != null)
                return d2;

            return d1;
        }

        public static string UppercaseWords(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
