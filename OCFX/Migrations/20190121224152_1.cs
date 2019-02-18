using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IP",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Replies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Comments",
                nullable: true);
        }
    }
}
