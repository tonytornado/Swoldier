using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class one : Migration
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
                name: "WorkoutId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Archetypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Story",
                table: "Archetypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strengths",
                table: "Archetypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weakness",
                table: "Archetypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AirCost = table.Column<int>(nullable: false),
                    Style = table.Column<int>(nullable: false),
                    Cooldown = table.Column<TimeSpan>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    Effect = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Warning = table.Column<string>(nullable: true),
                    ArchetypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Archetypes_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ArchetypeId",
                table: "Skills",
                column: "ArchetypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Archetypes");

            migrationBuilder.DropColumn(
                name: "Story",
                table: "Archetypes");

            migrationBuilder.DropColumn(
                name: "Strengths",
                table: "Archetypes");

            migrationBuilder.DropColumn(
                name: "Weakness",
                table: "Archetypes");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                nullable: true);

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
