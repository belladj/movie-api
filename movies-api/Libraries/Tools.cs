using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace movies_api.Libraries
{
    public class Tools
    {
        public static bool ProcessSqlStr(string inputString)
        {
            string text = "and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if (inputString != null && inputString != string.Empty)
                {
                    return new Regex("\\b(" + text + ")\\b", RegexOptions.IgnoreCase).IsMatch(inputString);
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        public static int ConvertObjectToIn32(object obj)
        {
            if (string.IsNullOrEmpty(obj + ""))
                return 0;
            if (string.IsNullOrEmpty(obj.ToString()))
                return 0;
            int result = 0;
            int.TryParse(obj.ToString(), out result);

            return result;
        }
        public static long ConvertObjectToIn64(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return 0L;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return 0L;
            }

            long result = 0L;
            long.TryParse(obj.ToString(), out result);
            return result;
        }
        public static double ConvertObjectToDouble(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return 0.0;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return 0.0;
            }

            double result = 0.0;
            double.TryParse(obj.ToString(), out result);
            return result;
        }
        public static float ConvertObjectToFloat(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return 0f;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return 0f;
            }

            float result = 0f;
            float.TryParse(obj.ToString(), out result);
            return result;
        }
        public static decimal ConvertObjectToDecimal(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return 0m;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return 0m;
            }

            decimal result = default(decimal);
            decimal.TryParse(obj.ToString(), out result);
            return result;
        }
        public static string ConvertObjectToString(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return "";
            }

            if (ProcessSqlStr(obj.ToString()))
            {
                return "";
            }

            return obj.ToString();
        }
        public static bool ConvertObjectToBool(object obj)
        {
            if (string.IsNullOrEmpty(obj?.ToString() ?? ""))
            {
                return false;
            }

            if (obj.ToString() == "1")
            {
                return true;
            }

            if (obj.ToString() == "0")
            {
                return false;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return false;
            }

            bool result = false;
            bool.TryParse(obj.ToString(), out result);
            return result;
        }
        public static DateTime ConvertObjectToDateTime(object obj)
        {
            if (obj == null)
            {
                return DateTime.MinValue;
            }

            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return DateTime.MinValue;
            }

            DateTime result = DateTime.MinValue;
            DateTime.TryParse(obj.ToString(), out result);
            return result;
        }
        public static List<int> ConvertStringToListInt32(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            List<int> list = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                int result = 0;
                int.TryParse(str.Substring(i, 1), out result);
                list.Add(result);
            }

            return list;
        }
        public static string GetDateTimeNowToString()
        {
            DateTime dateTime = default(DateTime);
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}