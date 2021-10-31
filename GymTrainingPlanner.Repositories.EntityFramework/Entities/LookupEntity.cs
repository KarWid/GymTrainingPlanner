namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LookupEntity : BaseEntity<int>
    {
        [Required]
        [StringLength(Constants.StringLength.DEFAULT_SHORT_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(Constants.StringLength.DEFAULT_AVERAGE_LENGTH)]
        public string Value { get; set; }

        [Required]
        [StringLength(Constants.StringLength.DEFAULT_SHORT_LENGTH)]
        public string GroupName { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<ExerciseDefinitionEntity> Exercises { get; set; }
        public virtual IEnumerable<TrainingPlanEntity> TrainingPlans { get; set; }
    }
}
