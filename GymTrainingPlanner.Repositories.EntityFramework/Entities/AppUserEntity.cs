namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class AppUserEntity : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // TODO: check Token length
        public string Token { get; set; }

        public virtual IEnumerable<TrainingPlanEntity> TrainingPlans { get; set; }
    }
}