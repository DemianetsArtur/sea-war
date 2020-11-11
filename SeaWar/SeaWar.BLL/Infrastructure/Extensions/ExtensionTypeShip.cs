using System;
using System.Reflection;

namespace SeaWar.BLL.Infrastructure.Extensions
{
    public static class ExtensionTypeShip
    {
        public static T GetAttributOfType<T>(this Enum enumVal) where T : Attribute
        {
            Type type = enumVal.GetType();
            MemberInfo[] memberInfo = type.GetMember(enumVal.ToString());
            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
