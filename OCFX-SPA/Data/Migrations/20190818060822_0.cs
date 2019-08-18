using Microsoft.EntityFrameworkCore.Migrations;

namespace OCFX_SPA.Data.Migrations
{
    public partial class _0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_Encounter_AntagonistId",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_Diet_DietId",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounter_Quest_QuestId",
                table: "Encounter");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounter_Campaign_CampaignId",
                table: "Encounter");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Campaign_CampaignId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Archetype_ClassId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Quest_QuestId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Campaign_CampaignId",
                table: "Quest");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Archetype_ArchetypeId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgram_Campaign_CampaignId",
                table: "WorkoutProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quest",
                table: "Quest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archetype",
                table: "Archetype");

            migrationBuilder.RenameTable(
                name: "Quest",
                newName: "Quests");

            migrationBuilder.RenameTable(
                name: "Campaign",
                newName: "Campaigns");

            migrationBuilder.RenameTable(
                name: "Archetype",
                newName: "Archetypes");

            migrationBuilder.RenameIndex(
                name: "IX_Quest_CampaignId",
                table: "Quests",
                newName: "IX_Quests_CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaign_DietId",
                table: "Campaigns",
                newName: "IX_Campaigns_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaign_AntagonistId",
                table: "Campaigns",
                newName: "IX_Campaigns_AntagonistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quests",
                table: "Quests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archetypes",
                table: "Archetypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Encounter_AntagonistId",
                table: "Campaigns",
                column: "AntagonistId",
                principalTable: "Encounter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Diet_DietId",
                table: "Campaigns",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounter_Quests_QuestId",
                table: "Encounter",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounter_Campaigns_CampaignId",
                table: "Encounter",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Campaigns_CampaignId",
                table: "Profile",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Archetypes_ClassId",
                table: "Profile",
                column: "ClassId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Quests_QuestId",
                table: "Profile",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Campaigns_CampaignId",
                table: "Quests",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Archetypes_ArchetypeId",
                table: "Skill",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgram_Campaigns_CampaignId",
                table: "WorkoutProgram",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Encounter_AntagonistId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Diet_DietId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounter_Quests_QuestId",
                table: "Encounter");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounter_Campaigns_CampaignId",
                table: "Encounter");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Campaigns_CampaignId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Archetypes_ClassId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Quests_QuestId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Campaigns_CampaignId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Archetypes_ArchetypeId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgram_Campaigns_CampaignId",
                table: "WorkoutProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quests",
                table: "Quests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archetypes",
                table: "Archetypes");

            migrationBuilder.RenameTable(
                name: "Quests",
                newName: "Quest");

            migrationBuilder.RenameTable(
                name: "Campaigns",
                newName: "Campaign");

            migrationBuilder.RenameTable(
                name: "Archetypes",
                newName: "Archetype");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_CampaignId",
                table: "Quest",
                newName: "IX_Quest_CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_DietId",
                table: "Campaign",
                newName: "IX_Campaign_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_AntagonistId",
                table: "Campaign",
                newName: "IX_Campaign_AntagonistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quest",
                table: "Quest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archetype",
                table: "Archetype",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_Encounter_AntagonistId",
                table: "Campaign",
                column: "AntagonistId",
                principalTable: "Encounter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_Diet_DietId",
                table: "Campaign",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounter_Quest_QuestId",
                table: "Encounter",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounter_Campaign_CampaignId",
                table: "Encounter",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Campaign_CampaignId",
                table: "Profile",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Archetype_ClassId",
                table: "Profile",
                column: "ClassId",
                principalTable: "Archetype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Quest_QuestId",
                table: "Profile",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Campaign_CampaignId",
                table: "Quest",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Archetype_ArchetypeId",
                table: "Skill",
                column: "ArchetypeId",
                principalTable: "Archetype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgram_Campaign_CampaignId",
                table: "WorkoutProgram",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
