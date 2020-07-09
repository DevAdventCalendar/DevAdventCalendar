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

                context.Database.ExecuteSqlRaw(
                    "INSERT INTO AspNetRoles ([Id],[ConcurrencyStamp],[Name],[NormalizedName]) " +
                    "VALUES('1','57c244a4-9d0a-4b58-9997-d1c920ca6a21','Admin',null)");

                context.Database.ExecuteSqlRaw(
                    "INSERT INTO AspNetUsers ([Id],[AccessFailedCount],[ConcurrencyStamp],[Email],[EmailConfirmed],[LockoutEnabled],[LockoutEnd],[NormalizedEmail],[NormalizedUserName]," +
                    "[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName],[EmailNotificationsEnabled],[PushNotificationsEnabled])" +
                    "VALUES ('c611530e-bebd-41a9-ace2-951550edbfa0', 0, 'e4de8553-5c8e-46b1-ab5a-19052617edd2', 'devadventcalendar@gmail.com'," +
                    "true, false, null, 'DEVADVENTCALENDAR@GMAIL.COM','DEVADVENTCALENDAR@GMAIL.COM', 'AQAAAAEAACcQAAAAEFkQXt1OKqtskYErBhopRhGBvLkB+eNCGS1ana7FXWzKzUlCIhQxGCzZfXQuDNMk1A=='," +
                    "null, false,'DWAXVN2W4U5J3KSGI7V7KPLU3GBWFVJL', false, 'devadventcalendar@gmail.com',false,false)");

                context.Database.ExecuteSqlRaw(
                    "INSERT INTO AspNetUserRoles ([UserId],[RoleId])" +
                    "VALUES ('c611530e-bebd-41a9-ace2-951550edbfa0','1')");
            }
        }
    }
}
