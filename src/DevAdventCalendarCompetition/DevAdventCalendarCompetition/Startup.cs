using System;
using System.Globalization;
using System.IO;
using AutoMapper;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace DevAdventCalendarCompetition
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables();

            this.Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UpdateDatabase();
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error?statusCode={0}");
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
        [Obsolete("Should be fixed During Refactor")]

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(this.Configuration.GetValue<string>("DataProtection:Keys")))
                .SetApplicationName("DevAdventCalendar");

            ServicesStartup.Configure(services, this.Configuration);

            services
                .RegisterDatabase(this.Configuration)
                .RegisterServices(this.Configuration)
                .AddAutoMapper(typeof(Startup))
                .AddExternalLoginProviders(this.Configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "QuickApp API", Version = "v1" });
                })
                .AddMvc(options => options.EnableEndpointRouting = false);

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.ConfigureOptions(this.Configuration);

            services.AddHttpClient();
        }
    }
}