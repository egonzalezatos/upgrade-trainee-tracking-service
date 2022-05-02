using Microsoft.EntityFrameworkCore;
using Sdk.Infrastructure.Extensions;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.InMemory.Seeds;

namespace Upgrade.TraineeTracking.InMemory
{
    public class InMemoryContext : DbContext
    {
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }

        public InMemoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityDesigns(typeof(InMemoryContext));

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder builder)
        {
            UserCoursesSeeder.Seed(builder);
            UserPlansSeeder.Seed(builder);
        }
    }
}