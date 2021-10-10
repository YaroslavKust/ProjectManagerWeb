using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace ProjectManager.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDataContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ManagerContext>(_ =>
                new ManagerContext(config.GetConnectionString("sqlConnection")));
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = jwtSettings["key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("issuer").Value,
                    ValidAudience = jwtSettings.GetSection("audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Project Manager API",
                Description = "API for current application"
            }));
        }
    }
}