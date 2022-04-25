using DiaryWPF.Models.Configurations;
using DiaryWPF.Models.Domains;
using DiaryWPF.Properties;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DiaryWPF
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString = $@"
Server={Settings.Default.ServerName};
Database={Settings.Default.DatabaseName};
User Id={Settings.Default.UserName};
Password={Settings.Default.Password}
";

        public ApplicationDbContext()
            : base(_connectionString)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}