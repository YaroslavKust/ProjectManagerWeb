using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectManager.API.ActionFilters;
using ProjectManager.API.Extensions;
using ProjectManager.API.Services.Authentication;
using ProjectManager.DAL.UnitOfWorks;

namespace ProjectManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureDataContext(Configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.ConfigureSwagger();
            services.ConfigureAuthentication(Configuration);
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ValidateProjectExistsAttribute>();
            services.AddScoped<ValidateTaskExistsAttribute>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "ProjectManager API v1"));

            app.ConfigureExceptionsHandler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
