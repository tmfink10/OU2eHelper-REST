using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class RefactorPlayerAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_EmpathyId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_PerceptionId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_StrengthId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_WillpowerId",
                table: "PlayerCharacters");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacters_EmpathyId",
                table: "PlayerCharacters");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacters_PerceptionId",
                table: "PlayerCharacters");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacters_StrengthId",
                table: "PlayerCharacters");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacters_WillpowerId",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "EmpathyId",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "PerceptionId",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "StrengthId",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "WillpowerId",
                table: "PlayerCharacters");

            migrationBuilder.AddColumn<int>(
                name: "PlayerCharacterId",
                table: "PlayerAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAttributes_PlayerCharacterId",
                table: "PlayerAttributes",
                column: "PlayerCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes",
                column: "PlayerCharacterId",
                principalTable: "PlayerCharacters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes");

            migrationBuilder.DropIndex(
                name: "IX_PlayerAttributes_PlayerCharacterId",
                table: "PlayerAttributes");

            migrationBuilder.DropColumn(
                name: "PlayerCharacterId",
                table: "PlayerAttributes");

            migrationBuilder.AddColumn<int>(
                name: "EmpathyId",
                table: "PlayerCharacters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerceptionId",
                table: "PlayerCharacters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StrengthId",
                table: "PlayerCharacters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WillpowerId",
                table: "PlayerCharacters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_EmpathyId",
                table: "PlayerCharacters",
                column: "EmpathyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_PerceptionId",
                table: "PlayerCharacters",
                column: "PerceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_StrengthId",
                table: "PlayerCharacters",
                column: "StrengthId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_WillpowerId",
                table: "PlayerCharacters",
                column: "WillpowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_EmpathyId",
                table: "PlayerCharacters",
                column: "EmpathyId",
                principalTable: "PlayerAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_PerceptionId",
                table: "PlayerCharacters",
                column: "PerceptionId",
                principalTable: "PlayerAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_StrengthId",
                table: "PlayerCharacters",
                column: "StrengthId",
                principalTable: "PlayerAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_PlayerAttributes_WillpowerId",
                table: "PlayerCharacters",
                column: "WillpowerId",
                principalTable: "PlayerAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
