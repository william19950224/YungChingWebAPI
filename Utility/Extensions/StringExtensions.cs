using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Utility.Extensions {
    public static class StringExtensions {
        /// <summary>
        /// 取得 Enum 列舉 Attribute Description 設定值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescriptionText(this Enum source) {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
}
