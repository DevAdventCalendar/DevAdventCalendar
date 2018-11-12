using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace DevAdventCalendarCompetition.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBaseTestRepository, BaseTestRepository>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            var config = Configuration.GetSection("StringHasher");
            services.AddTransient<StringHasher>(new HashParameters(int.Parse(config.Key), Encoding.ASCII.GetBytes(config.Value));

            services.AddAutoMapper();
        }
    }
}