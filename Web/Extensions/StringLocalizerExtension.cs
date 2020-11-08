using Microsoft.Extensions.Localization;

namespace Web.Extensions
{
    public static class StringLocalizerExtension
    {
        public static string GetValue<T>(this IStringLocalizer<T> localizer, string key)
        {
            return localizer[key].Value;
        }
    }
}
