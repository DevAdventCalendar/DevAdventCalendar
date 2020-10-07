using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Resources;
using DevAdventCalendarCompetition.Services;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DevAdventCalendarCompetition.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddAdventConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.Configure<AdventSettings>(settings =>
                {
                    string defaultDateTimeFormat = "dd-MM-yyyy";
                    var adventEndDate = configuration.GetSection("Competition:EndDate").Value;
                    var adventStartDate = configuration.GetSection("Competition:StartDate").Value;
                    var isValidStartDateTime = DateTimeOffset.TryParseExact(adventStartDate, defaultDateTimeFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var startDate);
                    var isValidEndDateTime = DateTimeOffset.TryParseExact(adventEndDate, defaultDateTimeFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var endDate);

                    if (!isValidStartDateTime || !isValidEndDateTime)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionsMessages.WrongFormatOfDate, defaultDateTimeFormat));
                    }

                    settings.StartDate = startDate.DateTime;
                    settings.EndDate = endDate.DateTime;
                });

            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AdventSettings>>().Value);
            return services;
        }

        public static IServiceCollection AddTestConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddOptions<TestSettings>()
                .Bind(configuration.GetSection("Test"))
                .ValidateDataAnnotations();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<TestSettings>>().Value);
            return services;
        }

        public static IServiceCollection AddGoogleCalendarConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddOptions<GoogleCalendarSettings>()
                .Bind(configuration.GetSection("Authentication:Google:CalendarAPI:Calendar"))
                .ValidateDataAnnotations();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<GoogleCalendarSettings>>().Value);
            return services;
        }

        public static IServiceCollection RegisterGoogleHttpClient(this IServiceCollection services)
        {
            var googleCalendarBaseUri = @"https://www.googleapis.com/calendar/v3/";
            services.AddHttpContextAccessor();
            services.AddHttpClient<IGoogleCalendarService, GoogleCalendarService>(
                async (services, client) =>
                {
                    var accessor = services.GetRequiredService<IHttpContextAccessor>();
                    var accessToken = await accessor.HttpContext.GetTokenAsync("Calendar", "access_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    client.BaseAddress = new Uri(googleCalendarBaseUri);
                });
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
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IAdventService, AdventService>();
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
            .AddOAuth("Calendar", googleOptions =>
            {
                googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                var googleCalendarConfig = configuration.GetSection("Authentication:Google:CalendarAPI");
                googleOptions.AuthorizationEndpoint = googleCalendarConfig["AuthorizationEndpoint"];
                googleOptions.TokenEndpoint = googleCalendarConfig["TokenEndpoint"];
                googleOptions.Scope.Add(googleCalendarConfig["CalendarScope"]);
                googleOptions.Scope.Add(googleCalendarConfig["EventsScope"]);
                googleOptions.CallbackPath = googleCalendarConfig["CallbackPath"];
                googleOptions.UsePkce = Convert.ToBoolean(googleCalendarConfig["UsePkce"], CultureInfo.InvariantCulture);
                googleOptions.SaveTokens = Convert.ToBoolean(googleCalendarConfig["SaveTokens"], CultureInfo.InvariantCulture);
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