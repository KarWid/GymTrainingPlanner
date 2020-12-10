namespace GymTrainingPlanner.Api.Services
{
    using System;

    public interface ITimeService
    {
        DateTime Now();
        DateTime UtcNow();
    }

    public class TimeService : ITimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
