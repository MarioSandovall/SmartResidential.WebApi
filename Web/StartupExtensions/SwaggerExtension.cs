using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Web.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Residential", Version = "v1" });
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     In = ParameterLocation.Header,
                     Description = "Please insert JWT with Bearer into field",
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey
                 });
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Id = "Bearer",
                         Type = ReferenceType.SecurityScheme
                       }
                      },
                      new string[] { }
                    }
                   });
             });
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Residential");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
