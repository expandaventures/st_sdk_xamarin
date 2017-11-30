using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SinTrafico.Helpers
{
    internal static class EnumDescriptionHelpers
    {
        static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var typeInfo = enumVal.GetType().GetTypeInfo();
            var v = typeInfo.DeclaredMembers.First(x => x.Name == enumVal.ToString());
            return v.GetCustomAttribute<T>();
        }

        internal static string GetDescription(this Enum value)
        {
            var attr = GetAttributeOfType<DescriptionAttribute>(value);
            return attr != null ? attr.Description : string.Empty;
        }
    }
}
