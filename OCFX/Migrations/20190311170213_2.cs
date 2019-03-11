using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymAmenities_Gyms_GymId",
                table: "GymAmenities");

            migrationBuilder.DropIndex(
                name: "IX_GymAmenities_GymId",
                table: "GymAmenities");

            migrationBuilder.DropColumn(
                name: "EquipSkill",
                table: "GymAmenities");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "GymAmenities");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "WorkoutPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RelativeGyms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EquipmentId = table.Column<int>(nullable: false),
                    GymId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelativeGyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelativeGyms_GymAmenities_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "GymAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelativeGyms_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelativeGyms_EquipmentId",
                table: "RelativeGyms",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeGyms_GymId",
                table: "RelativeGyms",
                column: "GymId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelativeGyms");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "WorkoutPrograms");

            migrationBuilder.AddColumn<string>(
                name: "EquipSkill",
                table: "GymAmenities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "GymAmenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GymAmenities_GymId",
                table: "GymAmenities",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymAmenities_Gyms_GymId",
                table: "GymAmenities",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
