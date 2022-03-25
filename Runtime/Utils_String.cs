using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_String
    {
        static StringBuilder stringBuilder;
        static StringBuilder StringBuilder
        {
            get
            {
                if (stringBuilder == null) stringBuilder = new StringBuilder();
                else stringBuilder.Clear();
                return stringBuilder;
            }
        }

        public static string Append(this string first, string second) => StringBuilder.Append(first).Append(second).ToString();
        public static string Append(this string first, string second, string third) => StringBuilder.Append(first).Append(second).Append(third).ToString();
        public static string Append(this string first, string second, string third, string quarter) => StringBuilder.Append(first).Append(second).Append(third).Append(quarter).ToString();
        public static string Append(this string first, string second, string third, string quarter, string fifth) => StringBuilder.Append(first).Append(second).Append(third).Append(quarter).Append(fifth).ToString();
        public static string Append(this string first, string second, string third, string quarter, string fifth, string sixth) => StringBuilder.Append(first).Append(second).Append(third).Append(quarter).Append(fifth).Append(sixth).ToString();
        public static string Append(this string first, string second, string third, string quarter, string fifth, string sixth, string seventh) => StringBuilder.Append(first).Append(second).Append(third).Append(quarter).Append(fifth).Append(sixth).Append(seventh).ToString();
    }
}

