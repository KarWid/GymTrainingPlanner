using Microsoft.EntityFrameworkCore.Migrations;

namespace GymTrainingPlanner.Repositories.EntityFramework.Migrations
{
    public partial class AddedAdditionalInformationToExerciseDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "ExerciseDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "ExerciseDetails");
        }
    }
}
