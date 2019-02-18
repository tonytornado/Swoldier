using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestLogs_Quests_QuestId",
                table: "QuestLogs");

            migrationBuilder.DropIndex(
                name: "IX_QuestLogs_QuestId",
                table: "QuestLogs");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "QuestLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestLogs_CampaignId",
                table: "QuestLogs",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLogs_Campaigns_CampaignId",
                table: "QuestLogs",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestLogs_Campaigns_CampaignId",
                table: "QuestLogs");

            migrationBuilder.DropIndex(
                name: "IX_QuestLogs_CampaignId",
                table: "QuestLogs");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "QuestLogs");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLogs_QuestId",
                table: "QuestLogs",
                column: "QuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLogs_Quests_QuestId",
                table: "QuestLogs",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
