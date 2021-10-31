namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class AppUserEntity : IdentityUser<Guid>
    {
        [StringLength(Constants.StringLength.DEFAULT_SHORT_LENGTH)]
        public string FirstName { get; set; }

        [StringLength(Constants.StringLength.DEFAULT_SHORT_LENGTH)]
        public string LastName { get; set; }

        // TODO: check Token length
        [StringLength(Constants.StringLength.DEFAULT_AVERAGE_LENGTH)]
        public string Token { get; set; }

        public virtual IEnumerable<TrainingPlanEntity> TrainingPlans { get; set; }
    }
}