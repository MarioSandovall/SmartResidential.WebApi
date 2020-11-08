using System;
using System.Linq;

namespace Domain.Utils
{
    internal static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            var genericEnumType = @enum.GetType();
            var memberInfo = genericEnumType.GetMember(@enum.ToString());
            if ((memberInfo.Length > 0))
            {
                var attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((attribs.Any()))
                {
                    return ((System.ComponentModel.DescriptionAttribute)attribs.ElementAt(0)).Description;
                }
            }
            return @enum.ToString();
        }
    }
}
