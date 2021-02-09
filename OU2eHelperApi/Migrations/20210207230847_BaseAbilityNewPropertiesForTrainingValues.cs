using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class BaseAbilityNewPropertiesForTrainingValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiesTrainingValuesCoded",
                table: "BaseAbilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiesTrainingValuesOptionsCoded",
                table: "BaseAbilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "ModifiesTrainingValuesCoded",
                value: "First Aid Kit,Medical Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 6,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 7,
                column: "ModifiesTrainingValuesCoded",
                value: "Athletic Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 9,
                column: "ModifiesTrainingValuesCoded",
                value: "First Aid Kit");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 10,
                column: "ModifiesTrainingValuesCoded",
                value: "Vehicles");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 12,
                column: "ModifiesTrainingValuesCoded",
                value: "Vehicles");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ModifiesTrainingValuesCoded", "ModifiesTrainingValuesOptionsCoded" },
                values: new object[] { "Tools", "Bludgeon,Piercing,Slashing" });

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 19,
                column: "ModifiesTrainingValuesCoded",
                value: "First Aid Kit,Medical Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 21,
                column: "ModifiesTrainingValuesCoded",
                value: "Reconnaissance Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 22,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 24,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 25,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 26,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 29,
                column: "ModifiesTrainingValuesCoded",
                value: "Swimming/Diving");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 32,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 33,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 34,
                column: "ModifiesTrainingValuesCoded",
                value: "Firefighting,First Aid Kit");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 35,
                column: "ModifiesTrainingValuesCoded",
                value: "Throwing");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 36,
                column: "ModifiesTrainingValuesCoded",
                value: "Pistol");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 41,
                column: "ModifiesTrainingValuesCoded",
                value: "Command Apparatus");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 42,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 43,
                column: "ModifiesTrainingValuesOptionsCoded",
                value: "Pistol,Long Gun");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 44,
                column: "ModifiesTrainingValuesCoded",
                value: "Martial Arts");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 46,
                column: "ModifiesTrainingValuesCoded",
                value: "Climbing Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 49,
                column: "ModifiesTrainingValuesCoded",
                value: "Bludgeon");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 50,
                column: "ModifiesTrainingValuesCoded",
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 52,
                column: "ModifiesTrainingValuesCoded",
                value: "Value");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 53,
                column: "ModifiesTrainingValuesCoded",
                value: "Reconnaissance Gear,First Aid Kit");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 59,
                column: "ModifiesTrainingValuesCoded",
                value: "First Aid Kit,Medical Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 60,
                column: "ModifiesTrainingValuesCoded",
                value: "Survival Kit");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 66,
                column: "ModifiesTrainingValuesCoded",
                value: "Climbing Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 67,
                column: "ModifiesTrainingValuesCoded",
                value: "Survival Kit,Tools");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 68,
                column: "ModifiesTrainingValuesCoded",
                value: "Archery Gear");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 70,
                column: "ModifiesTrainingValuesCoded",
                value: "Long Gun");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 71,
                column: "ModifiesTrainingValuesOptionsCoded",
                value: "Bludgeon,Piercing,Slashing");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 72,
                column: "ModifiesTrainingValuesCoded",
                value: "Pistol");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 73,
                column: "ModifiesTrainingValuesCoded",
                value: "Vehicles");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 74,
                column: "ModifiesTrainingValuesOptionsCoded",
                value: "Long Gun,Pistol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiesTrainingValuesCoded",
                table: "BaseAbilities");

            migrationBuilder.DropColumn(
                name: "ModifiesTrainingValuesOptionsCoded",
                table: "BaseAbilities");
        }
    }
}
