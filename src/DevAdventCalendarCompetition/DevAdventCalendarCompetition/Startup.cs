using AutoMapper;
using DevAdventCalendarCompetition.Extensions;
using DevAdventCalendarCompetition.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DevAdventCalendarCompetition
{
    public class Startup
    {
        private const string DockerEnvName = "Docker";
        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterDatabase(Configuration)
                .RegisterServices()
                .AddAutoMapper()
                .RegisterMapping()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "QuickApp API", Version = "v1" });
                })
                .AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.EnvironmentName.Equals(DockerEnvName))
            {
                app.ApplicationServices.GetService<ApplicationDbContext>().Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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
    }
}