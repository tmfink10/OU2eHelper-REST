using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class UpdatePlayerSkillRenameAdditionalValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalValue",
                table: "PlayerSkills",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PlayerSkills",
                newName: "AdditionalValue");
        }
    }
}
