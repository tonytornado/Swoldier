using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Replies",
                newName: "EntryId");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Posts",
                newName: "EntryId");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Comments",
                newName: "EntryId");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Replies",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "Replies",
                newName: "PosterId");

            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "Posts",
                newName: "PosterId");

            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "Comments",
                newName: "PosterId");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Replies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
