using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class RefactorPlayerCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerCharacterId",
                table: "PlayerAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes",
                column: "PlayerCharacterId",
                principalTable: "PlayerCharacters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerCharacterId",
                table: "PlayerAttributes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAttributes_PlayerCharacters_PlayerCharacterId",
                table: "PlayerAttributes",
                column: "PlayerCharacterId",
                principalTable: "PlayerCharacters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
