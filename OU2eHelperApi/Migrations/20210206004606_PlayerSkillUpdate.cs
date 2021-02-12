using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class PlayerSkillUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PlayerSkills",
                newName: "AttributeValue");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalValue",
                table: "PlayerSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "This character is nimble and gifted with quick reaction time. \r\n\r\nSkill Support: {Balance%, Dodge%}\r\n\r\nAdvancement Rate: +1 per Tier to {Balance%, Dodge%}\r\n\r\nMastery: Add +1 Defense when targeted with Ranged Attacks.", "This character is nimble and gifted with quick reaction time. <br/><br/>Skill Support: {Balance%, Dodge%}<br/><br/>Advancement Rate: +1 per Tier to {Balance%, Dodge%}<br/><br/>Mastery: Add +1 Defense when targeted with Ranged Attacks." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalValue",
                table: "PlayerSkills");

            migrationBuilder.RenameColumn(
                name: "AttributeValue",
                table: "PlayerSkills",
                newName: "Value");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "This character is nimble and gifted with quick reaction time. \r\n\r\nSkill Support: {Balance%, Dodge%,}\r\n\r\nAdvancement Rate: +1 per Tier to {Balance%, Dodge%}\r\n\r\nMastery: Add +1 Defense when targeted with Ranged Attacks.", "This character is nimble and gifted with quick reaction time. <br/><br/>Skill Support: {Balance%, Dodge%,}<br/><br/>Advancement Rate: +1 per Tier to {Balance%, Dodge%}<br/><br/>Mastery: Add +1 Defense when targeted with Ranged Attacks." });
        }
    }
}
