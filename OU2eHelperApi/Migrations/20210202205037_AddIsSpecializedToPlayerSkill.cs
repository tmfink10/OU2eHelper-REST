using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class AddIsSpecializedToPlayerSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Supported",
                table: "PlayerSkills",
                newName: "IsSupported");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialized",
                table: "PlayerSkills",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecialized",
                table: "PlayerSkills");

            migrationBuilder.RenameColumn(
                name: "IsSupported",
                table: "PlayerSkills",
                newName: "Supported");
        }
    }
}
