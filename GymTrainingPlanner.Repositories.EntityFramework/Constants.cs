namespace GymTrainingPlanner.Repositories.EntityFramework
{
    public static class Constants
    {
        public static class Role
        {
            public const string ADMIN = "Admin";
            public const string CUSTOMER = "Customer";
        }

        public static class LookupGroupNames
        {
            public const string EXERCISE_NAME = "exercise-name";
        }

        public static class PostgresFunctions
        {
            public const string UUID_GENERATE_V4 = "uuid_generate_v4()";
            public const string UUID_COLUMN_TYPE = "uuid";
        }
    }
}
