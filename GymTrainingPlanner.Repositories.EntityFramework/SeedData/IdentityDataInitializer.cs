namespace GymTrainingPlanner.Repositories.EntityFramework.SeedData
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Repositories.EntityFramework.Enums;

    public static class IdentityDataInitializer
    {
        public static void SeedData(RoleManager<AppRoleEntity> roleManager)
        {
            SeedRoles(roleManager);
        }

        private static void SeedRoles(RoleManager<AppRoleEntity> roleManager)
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
