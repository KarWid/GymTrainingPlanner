namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;

    public class TrainingPlanEntity : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public string Name { get; set; }

        public virtual AppUserEntity User {get; set;}

        public virtual IEnumerable<TrainingDayDefinitionEntity> TrainingDays { get; set; }
    }
}
