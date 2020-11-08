using Microsoft.AspNetCore.Builder;

namespace Web.StartupExtensions
{
    public static class LocalizationExtension
    {
        public static void Uselocalization(this IApplicationBuilder app)
        {
            const string english = "en-US";
            const string spanish = "es-MX";

            var cultures = new[] { spanish, english };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(english)
                .AddSupportedCultures(cultures)
                .AddSupportedUICultures(cultures);

            app.UseRequestLocalization(localizationOptions);
        }
    }
}
