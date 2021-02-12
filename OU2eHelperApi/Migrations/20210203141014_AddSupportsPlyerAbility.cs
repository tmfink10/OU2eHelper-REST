using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class AddSupportsPlyerAbility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseSkills_PlayerAbilities_PlayerAbilityId",
                table: "BaseSkills");

            migrationBuilder.DropIndex(
                name: "IX_BaseSkills_PlayerAbilityId",
                table: "BaseSkills");

            migrationBuilder.DropColumn(
                name: "PlayerAbilityId",
                table: "BaseSkills");

            migrationBuilder.AddColumn<int>(
                name: "PlayerAbilityId",
                table: "PlayerSkills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkills_PlayerAbilityId",
                table: "PlayerSkills",
                column: "PlayerAbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSkills_PlayerAbilities_PlayerAbilityId",
                table: "PlayerSkills",
                column: "PlayerAbilityId",
                principalTable: "PlayerAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSkills_PlayerAbilities_PlayerAbilityId",
                table: "PlayerSkills");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSkills_PlayerAbilityId",
                table: "PlayerSkills");

            migrationBuilder.DropColumn(
                name: "PlayerAbilityId",
                table: "PlayerSkills");

            migrationBuilder.AddColumn<int>(
                name: "PlayerAbilityId",
                table: "BaseSkills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseSkills_PlayerAbilityId",
                table: "BaseSkills",
                column: "PlayerAbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSkills_PlayerAbilities_PlayerAbilityId",
                table: "BaseSkills",
                column: "PlayerAbilityId",
                principalTable: "PlayerAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
