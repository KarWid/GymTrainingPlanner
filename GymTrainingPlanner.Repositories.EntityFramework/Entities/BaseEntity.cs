namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }

    public class ExtendedBaseEntity<T> : BaseEntity<T>
    {
        public Guid ModifiedBy { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }
    }
}
