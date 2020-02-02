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
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error?statusCode={0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UpdateDatabase();
            app.UseForwardedHeaders();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpMethodOverride();

            app.UseHttpsRequestScheme();

            app.UseRouting();

            // app.UseAuthorization();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickApp API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

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
                    c.SwaggerDoc("v1", new Info { Title = "DevAdventCalendar API", Version = "v1" });
                })
                .AddMvc(options => options.EnableEndpointRouting = false);

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.ConfigureOptions(this.Configuration);

            services.AddHttpClient();
        }
    }
}