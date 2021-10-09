namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class AppUserEntity : IdentityUser<Guid>
    {
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        // TODO: check Token length
        [StringLength(255)]
        public string Token { get; set; }

        public virtual IEnumerable<TrainingPlanEntity> TrainingPlans { get; set; }
    }
}