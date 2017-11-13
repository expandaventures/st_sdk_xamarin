using System;
using System.ComponentModel;

namespace SinTrafico.Helpers
{
    internal static class TransportTypeHelpers
    {
        static string GetCustomDescription(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        internal static string GetDescription(this Enum value)
        {
            return GetCustomDescription(value);
        }
    }
}
