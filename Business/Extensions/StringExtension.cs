namespace Business.Extensions
{
    public static class StringExtension
    {
        public static string RemoveSpace(this string value)
        {
            return value?.Trim();
        }
    }
}
