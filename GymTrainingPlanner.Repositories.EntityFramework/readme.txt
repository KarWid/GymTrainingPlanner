I. If a connectionString is null or empty, then setup the environment variable by:
$env:GYM_TRAINING_PLANNER_CONNECTION_STRING='{database connection string here}'

II. GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO "GymTrainingPlannerUser"

1. To add migration
dotnet ef migrations add {Migration-Name} --project GymTrainingPlanner.Repositories.EntityFramework --startup-project GymTrainingPlanner.Api

2. To update database
Update-database