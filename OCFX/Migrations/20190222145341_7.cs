using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ProfileId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_EntryId",
                table: "Replies",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_EntryId",
                table: "Posts",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EntryId",
                table: "Comments",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_EntryId",
                table: "Comments",
                column: "EntryId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Profiles_EntryId",
                table: "Posts",
                column: "EntryId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Profiles_EntryId",
                table: "Replies",
                column: "EntryId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Profiles_EntryId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Profiles_EntryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Profiles_EntryId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_EntryId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Posts_EntryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EntryId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ProfileId",
                table: "Messages",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
