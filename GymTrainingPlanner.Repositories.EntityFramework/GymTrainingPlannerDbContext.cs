namespace GymTrainingPlanner.Repositories.EntityFramework
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations;

    public class GymTrainingPlannerDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid>
    {
        public DbSet<ExerciseDetailEntity> ExerciseDetails { get; set; }
        public DbSet<ExerciseEntity> Exercises { get; set; }
        public DbSet<LookupEntity> Lookups { get; set; }
        public DbSet<TrainingDayEntity> TrainingDays { get; set; }
        public DbSet<TrainingPlanEntity> TrainingPlans { get; set; }

        public GymTrainingPlannerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AppUserConfiguration.Configure(builder);
            ExerciseConfiguration.Configure(builder);
            LookupConfiguration.Configure(builder);
            TrainingDayConfiguration.Configure(builder);
            TrainingPlanConfiguration.Configure(builder);
        }
    }
}
