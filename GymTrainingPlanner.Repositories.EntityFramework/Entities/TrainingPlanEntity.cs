namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TrainingPlanEntity : ExtendedBaseEntity<Guid>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTimeOffset DateFrom { get; set; }

        public int TrainingTypeId { get; set; }
        public virtual LookupEntity TrainingType { get; set; }

        public Guid UserId { get; set; }
        public virtual AppUserEntity User {get; set;}

        public virtual IEnumerable<TrainingDayDefinitionEntity> TrainingDays { get; set; }
    }
}
