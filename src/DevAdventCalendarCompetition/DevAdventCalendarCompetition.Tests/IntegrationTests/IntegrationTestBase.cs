using System;
using DevAdventCalendarCompetition.Repository.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected IntegrationTestBase()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            this._connection = new SqliteConnection(connectionString);
            this._connection.Open();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(this._connection);

            this.ContextOptions = builder.Options;

            this.Seed();
        }

        protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._connection.Dispose();
        }

        private void Seed()
        {
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Database.EnsureCreated();

                var roleId = "1";
                var roleConcurrencyStamp = "57c244a4-9d0a-4b58-9997-d1c920ca6a21";
                var roleName = "Admin";
                var normalizedRoleName = "ADMIN";

                context.Database.ExecuteSqlInterpolated(
                    $@"INSERT INTO AspNetRoles ([Id],[ConcurrencyStamp],[Name],[NormalizedName]) 
                    VALUES({roleId},{roleConcurrencyStamp},{roleName},{normalizedRoleName})");

                var userId = "c611530e-bebd-41a9-ace2-951550edbfa0";
                var accessFailedCount = 0;
                var userConcurrencyStamp = "57c244a4-9d0a-4b58-9997-d1c920ca6a21";
                var email = "devadventcalendar@gmail.com";
                var emailConfirmed = true;
                var lockoutEnabled = false;
                DateTimeOffset? lockoutEnd = null;
                var normalizedEmail = "DEVADVENTCALENDAR@GMAIL.COM";
                var normalizedUserName = "DEVADVENTCALENDAR@GMAIL.COM";
                var passwordHash =
                    "AQAAAAEAACcQAAAAEFkQXt1OKqtskYErBhopRhGBvLkB+eNCGS1ana7FXWzKzUlCIhQxGCzZfXQuDNMk1A==";
                int? phoneNumber = null;
                var phoneNumberConfirmed = false;
                var securityStamp = "DWAXVN2W4U5J3KSGI7V7KPLU3GBWFVJL";
                var twoFactorEnabled = false;
                var userName = "devadventcalendar@gmail.com";
                var emailNotificationsEnabled = false;
                var pushNotificationsEnabled = false;

                context.Database.ExecuteSqlInterpolated(
                    $@"INSERT INTO AspNetUsers ([Id],[AccessFailedCount],[ConcurrencyStamp],[Email],[EmailConfirmed],
                    [LockoutEnabled],[LockoutEnd],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],
                    [PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName],[EmailNotificationsEnabled],[PushNotificationsEnabled])
                    VALUES ({userId}, {accessFailedCount}, {userConcurrencyStamp}, {email},{emailConfirmed},{lockoutEnabled},
                    {lockoutEnd},{normalizedEmail},{normalizedUserName}, {passwordHash},{phoneNumber},{phoneNumberConfirmed},  
                    {securityStamp}, {twoFactorEnabled}, {userName},{emailNotificationsEnabled},{pushNotificationsEnabled})");

                context.Database.ExecuteSqlInterpolated(
                    $@"INSERT INTO AspNetUserRoles ([UserId],[RoleId])
                    VALUES ({userId},{roleId})");
            }
        }
    }
}
