namespace GymTrainingPlanner.Repositories.EntityFramework.SeedData
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Repositories.EntityFramework.Enums;

    public static class IdentityDataInitializer
    {
        public static void SeedData(RoleManager<AppRoleEntity> roleManager, bool isProduction = false)
        {
            if (isProduction)
            {
                SeedProductionData(roleManager);
                return;
            }

            SeedDevelopmentData(roleManager);
        }

        private static void SeedProductionData(RoleManager<AppRoleEntity> roleManager)
        {
            SeedProductionRoles(roleManager);
        }

        private static void SeedDevelopmentData(RoleManager<AppRoleEntity> roleManager)
        {
            SeedDevelopmentRoles(roleManager);
        }

        private static void SeedProductionRoles(RoleManager<AppRoleEntity> roleManager)
        {
            if (roleManager.Roles.Any())
            {
                return;
            }

            var appRoles = (AppRoleType[])Enum.GetValues(typeof(AppRoleType));
            appRoles.Select(_ => _.ToString()).ToList().ForEach(appRole =>
            {
                roleManager.CreateAsync(new AppRoleEntity { Name = appRole }).Wait();
            });
        }

        private static void SeedDevelopmentRoles(RoleManager<AppRoleEntity> roleManager)
        {
            if (roleManager.Roles.Any())
            {
                return;
            }

            var appRoles = (AppRoleType[])Enum.GetValues(typeof(AppRoleType));
            appRoles.Select(_ => _.ToString()).ToList().ForEach(appRole =>
            {
                roleManager.CreateAsync(new AppRoleEntity { Name = appRole }).Wait();
            });
        }
    }
}
