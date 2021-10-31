namespace GymTrainingPlanner.Common.Extensions
{
    using System;

    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        public static bool IsNullOrEmpty(this Guid? value)
        {
            return value == null || value == Guid.Empty;
        }
    }
}
