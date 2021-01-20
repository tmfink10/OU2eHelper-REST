using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class SeedPlayerCharacterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlayerCharacters",
                columns: new[] { "Id", "Age", "CargoCapacity", "CompetencePoints", "DamageThreshold", "EmpathyId", "FirstName", "GestaltLevel", "HealthPoints", "LastName", "Morale", "Notes", "PerceptionId", "Sex", "StrengthId", "SurvivalPoints", "WillpowerId" },
                values: new object[] { 1, 35, 0, 0, 0, null, "Trevor", 0, 0, "Fink", 0, "", null, null, null, 0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlayerCharacters",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
