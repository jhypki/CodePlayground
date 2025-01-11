using System.Runtime.Serialization;

namespace CodePlayground.Core.Helpers;

public static class EnumHelper
{
    public static string GetEnumStringValue<T>(T enumValue) where T : Enum
    {
        var type = typeof(T);
        var memberInfo = type.GetMember(enumValue.ToString()).FirstOrDefault();
        if (memberInfo == null) return null;

        var attribute = memberInfo
            .GetCustomAttributes(typeof(EnumMemberAttribute), false)
            .Cast<EnumMemberAttribute>()
            .FirstOrDefault();

        return attribute?.Value ?? enumValue.ToString();
    }
}