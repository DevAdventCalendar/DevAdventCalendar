using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevAdventCalendarCompetition.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
					.AddErrorDescriber<CustomIdentityErrorDescriber>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBaseTestRepository, BaseTestRepository>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IBaseTestService, BaseTestService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IManageService, ManageService>();
            services.AddTransient<IdentityService>();
            return services;
        }

        public static IServiceCollection RegisterMapping(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Test, TestDto>();
                cfg.CreateMap<TestDto, Test>();

                cfg.CreateMap<TestAnswer, TestAnswerDto>();
                cfg.CreateMap<TestAnswer, TestWithAnswerListDto>();
            });
            return services;
        }
    }
}