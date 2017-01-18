using System;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlTest
{
    class EnumHelper
    {
        public static string ToEnumString<T>(T value)
        {
            Type enumType = typeof(T);
            string name = Enum.GetName(enumType, value);
            EnumMemberAttribute enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public static T ToEnum<T>(string str)
        {
            Type enumType = typeof(T);
            foreach (string name in Enum.GetNames(enumType))
            {
                EnumMemberAttribute enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str)
                {
                    return (T)Enum.Parse(enumType, name);
                }
            }
            return default(T);
        }
    }
}
