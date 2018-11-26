using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace DevAdventCalendarCompetition.Services
{
    public static class ServicesStartup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBaseTestRepository, BaseTestRepository>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            var config = configuration.GetSection("StringHasher");
            var stringSalt = config.GetValue<string>("Salt");
            var hashParameters = new HashParameters(config.GetValue<int>("Iterations"), Encoding.ASCII.GetBytes(stringSalt));
            services.AddScoped<StringHasher>(sh => new StringHasher(hashParameters));

            services.AddAutoMapper();
        }
    }
}