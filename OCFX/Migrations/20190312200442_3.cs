using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageBoardPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoardPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBoardPosts_Gyms_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBoardPosts_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageBoardComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false),
                    BoardPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoardComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBoardComments_Gyms_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBoardComments_MessageBoardPosts_BoardPostId",
                        column: x => x.BoardPostId,
                        principalTable: "MessageBoardPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBoardComments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoardComments_BoardId",
                table: "MessageBoardComments",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoardComments_BoardPostId",
                table: "MessageBoardComments",
                column: "BoardPostId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoardComments_ProfileId",
                table: "MessageBoardComments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoardPosts_BoardId",
                table: "MessageBoardPosts",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoardPosts_ProfileId",
                table: "MessageBoardPosts",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageBoardComments");

            migrationBuilder.DropTable(
                name: "MessageBoardPosts");
        }
    }
}
