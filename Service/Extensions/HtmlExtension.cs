using Service.Utils;

namespace Service.Extensions
{
    internal static class HtmlExtension
    {
        public static string ToHtml(this string content)
        {
            return content.Replace(Characters.NewLine, HtmlTags.Br);
        }
    }
}
