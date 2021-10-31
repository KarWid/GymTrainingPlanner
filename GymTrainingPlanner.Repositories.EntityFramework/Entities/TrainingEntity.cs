namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TrainingEntity : ExtendedBaseEntity<Guid>
    {
        public DateTimeOffset Date { get; set; }

        /// serialized object
        [Required]
        [StringLength(Constants.StringLength.DEFAULT_LONG_LENGTH)]
        public string Content { get; set; }

        /// <summary>
        /// It should be null for other training plans than JimWendler531
        /// </summary>
        public int? JimWendler531MainLiftRepetitions { get; set; }

        public Guid TrainingDayDefinitionId { get; set; }
        public virtual TrainingDayDefinitionEntity TrainingDayDefinition { get; set; }
    }
}
