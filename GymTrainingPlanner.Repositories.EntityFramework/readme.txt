I. If a connectionString is null or empty, then setup the environment variable by:
$env:GYM_TRAINING_PLANNER_CONNECTION_STRING='{database connection string here}'

1. To add migration
dotnet ef migrations add {Migration-Name} --project GymTrainingPlanner.Repositories.EntityFramework --startup-project GymTrainingPlanner.Api

2. To update database
Update-database

3. To create database from scratch:
a) Create a postgres user: GymTrainingPlannerUser
b) Create and name a database as: GymTrainingPlanner and grant privilages or set the user as owner
c) Update the connectionString to the database defined in launchSettings.json and/or in environment variables
d) Create extension in the database: uuid-ossp
e) Run "Update-database"