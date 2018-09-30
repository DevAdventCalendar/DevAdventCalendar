//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using DevAdventCalendarCompetition.Models;

//namespace DevAdventCalendarCompetition
//{
//    //public class ApplicatioDbInitializer : MigrateDatabaseToLatestVersion<ApplicationDbContext, WrocSharpCompetition.Migrations.Configuration>
//    //{
//    //}

//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext()
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            LoadModelMappings(modelBuilder);
//        }

//        private void LoadModelMappings(ModelBuilder modelBuilder)
//        {
//            // Load all EntityTypeConfiguration<T> from current assembly and add to configurations
//            //var mapTypes = from t in typeof(ApplicationDbContext).Assembly.GetTypes()
//            //               where t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(MappingBase<>)
//            //               select t;

//            //foreach (var mapType in mapTypes)
//            //{
//            //    dynamic mapInstance = Activator.CreateInstance(mapType);
//            //    modelBuilder.Configurations.Add(mapInstance);
//            //}
//        }

//        public static ApplicationDbContext Create()
//        {
//            return new ApplicationDbContext();
//        }
//    }
//}