using System;
using System.IO;
using AutoMapper;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DevAdventCalendarCompetition
{
    public class Startup
    {
#pragma warning disable IDE0051 // Remove unused private members
        private const string DockerEnvName = "Docker";
#pragma warning restore IDE0051 // Remove unused private members

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
#pragma warning disable CA1062 // Validate arguments of public methods
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
#pragma warning restore CA1062 // Validate arguments of public methods
                .AddEnvironmentVariables();

            this.Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UpdateDatabase();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpMethodOverride();
            app.UseHttpsRequestScheme();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickApp API V1");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Should be fixed During Refactor
        [Obsolete]

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(this.Configuration.GetValue<string>("DataProtection:Keys")))
                .SetApplicationName("DevAdventCalendar");

            ServicesStartup.Configure(services, this.Configuration);

            services
                .RegisterDatabase(this.Configuration)
                .RegisterServices(this.Configuration)
                .AddAutoMapper()
                .AddExternalLoginProviders(this.Configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "QuickApp API", Version = "v1" });
                })
                .AddMvc();
        }
    }
}