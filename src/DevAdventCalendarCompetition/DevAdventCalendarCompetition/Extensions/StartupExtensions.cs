using System;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DevAdventCalendarCompetition.Extensions
{
    public static class StartupExtensions
    {
        // metoda validate configuration przyjumje parametry jak ta poniżej
        public static IServiceCollection ValidateConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string defaultDateTimeFormat = "dd-MM-yyyy";
#pragma warning disable CA1062 // Validate arguments of public methods
            var isAdventEndDate = configuration.GetSection("Competition:EndDate").Value;
#pragma warning restore CA1062 // Validate arguments of public methods
            var isAdventStartDate = configuration.GetSection("Competition:StartDate").Value;
            var correctStartDateTimeFormat = DateTimeOffset.TryParseExact(isAdventStartDate, defaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue);
            var correctEndDateTimeFormat = DateTimeOffset.TryParseExact(isAdventEndDate, defaultDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateValue1);

            if (!(correctStartDateTimeFormat || correctEndDateTimeFormat))
            {
                throw new InvalidOperationException(
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    $"Niepoprawny format daty zmiennej Configuration:EndDate lub Configuration:StartDate w appsettings " +
                    $"powinno byc ({defaultDateTimeFormat})");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            return services;
        }

        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<IUserTestAnswersRepository, UserTestAnswersRepository>();
            services.AddTransient<IResultsRepository, ResultsRepository>();

            services.AddScoped<DbInitializer>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddTransient<INotificationService, EmailNotificationService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IResultsService, ResultsService>();
            services.AddTransient<IManageService, ManageService>();
            services.AddTransient<IdentityService>();

            services.AddAutoMapper(typeof(AdminService));

            services.RegisterEmailService(configuration);
            services.RegisterStringHasherService(configuration);
            services.RegisterSwagger();

            return services;
        }

        public static IServiceCollection AddExternalLoginProviders(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddAuthentication()
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            })
            .AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            })
            .AddGitHub(githubOptions =>
            {
                githubOptions.ClientId = configuration["Authentication:GitHub:ClientId"];
                githubOptions.ClientSecret = configuration["Authentication:GitHub:ClientSecret"];
                githubOptions.Scope.Add("user:email");
            });

            return services;
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var init = scope.ServiceProvider.GetService<DbInitializer>();
                init.Seed();
            }
        }

        public static void UseHttpsRequestScheme(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.Use(next => context =>
            {
                context.Request.Scheme = "https";
                return next(context);
            });
        }

        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("pl-PL")
                };

                options.DefaultRequestCulture = new RequestCulture("pl-PL");
                options.SupportedCultures = supportedCultures;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.Configure<EmailNotificationOptions>(configuration.GetSection("EmailNotification"));

            return services;
        }

        private static IServiceCollection RegisterEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton<IEmailSender, EmailSender>(sender =>
            {
                var emailSender = new EmailSender
                {
                    Host = configuration.GetValue<string>("Email:Smtp:Host"),
                    Port = configuration.GetValue<int>("Email:Smtp:Port"),
                    UserName = configuration.GetValue<string>("Email:Smtp:UserName"),
                    Password = configuration.GetValue<string>("Email:Smtp:Password"),
                    From = configuration.GetValue<string>("Email:Smtp:From"),
                    Ssl = configuration.GetValue<bool?>("Email:Smtp:Ssl") ?? false,
                };

                return emailSender;
            });

            return services;
        }

        private static IServiceCollection RegisterStringHasherService(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var config = configuration.GetSection("StringHasher");
            var stringSalt = config.GetValue<string>("Salt");
            var hashParameters = new HashParameters(config.GetValue<int>("Iterations"), Encoding.ASCII.GetBytes(stringSalt));
            services.AddScoped(sh => new StringHasher(hashParameters));

            return services;
        }

        private static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevAdventCalendar API", Version = "v1" });
            });
            return services;
        }
    }
}