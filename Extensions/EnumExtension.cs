using System.ComponentModel;
using System.Reflection;

namespace MHXYWF.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum e)
    {
        MemberInfo? memberInfo = e.GetType().GetMembers().Where(p => p.Name == e.ToString()).FirstOrDefault();
        if (memberInfo is null) return "";
        return ((DescriptionAttribute)memberInfo.GetCustomAttribute(typeof(DescriptionAttribute))).Description;
    }
}
