using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Leader",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "ExGroup",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExType",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExName",
                table: "Exercises",
                newName: "Url");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "WorkoutPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Repetitions",
                table: "WorkoutPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "WorkoutPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExerType",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetedMuscles",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_CampaignId",
                table: "WorkoutPrograms",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_Campaigns_CampaignId",
                table: "WorkoutPrograms",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_Campaigns_CampaignId",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_CampaignId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Repetitions",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerType",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TargetedMuscles",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Exercises",
                newName: "ExName");

            migrationBuilder.AddColumn<int>(
                name: "Leader",
                table: "Gyms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExGroup",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExType",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
