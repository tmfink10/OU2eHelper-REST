using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class BaseAbilityAddShortName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "BaseAbilities",
                type: "nvarchar(22)",
                maxLength: 22,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShortName",
                value: "Acumen");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "ShortName",
                value: "ACLS");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "ShortName",
                value: "Agility");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "ShortName",
                value: "Animal Handling");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 5,
                column: "ShortName",
                value: "Animal Fellowship");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 6,
                column: "ShortName",
                value: "Artistic");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 7,
                column: "ShortName",
                value: "Athletic Cond.");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 8,
                column: "ShortName",
                value: "Autodidact");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 9,
                column: "ShortName",
                value: "First Aid");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 10,
                column: "ShortName",
                value: "Biker");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 11,
                column: "ShortName",
                value: "Billy Goat");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 12,
                column: "ShortName",
                value: "BMX");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 13,
                column: "ShortName",
                value: "Bookworm");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 14,
                column: "ShortName",
                value: "Bushcraft");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 15,
                column: "ShortName",
                value: "Caged Wisdom");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 16,
                column: "ShortName",
                value: "Charisma");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 17,
                column: "ShortName",
                value: "Civil Engineering");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 18,
                column: "ShortName",
                value: "Civilian");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 19,
                column: "ShortName",
                value: "Clinical Skills");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 20,
                column: "ShortName",
                value: "Combat Reflexes");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 21,
                column: "ShortName",
                value: "Concealment");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 22,
                column: "ShortName",
                value: "Contractor");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 23,
                column: "ShortName",
                value: "Cosmopolitan");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 24,
                column: "ShortName",
                value: "Craftsman");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 25,
                column: "ShortName",
                value: "Culinary Arts");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 26,
                column: "ShortName",
                value: "Custodian");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 27,
                column: "ShortName",
                value: "Damage Threshold");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 28,
                column: "ShortName",
                value: "Diligence");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 29,
                column: "ShortName",
                value: "Dive Certified");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 30,
                column: "ShortName",
                value: "Early Bird");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 31,
                column: "ShortName",
                value: "Efficient");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 32,
                column: "ShortName",
                value: "Electrical Eng.");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 33,
                column: "ShortName",
                value: "Farmer/Rancher");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 34,
                column: "ShortName",
                value: "Firefighter");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 35,
                column: "ShortName",
                value: "Grenadier");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 36,
                column: "ShortName",
                value: "Gunslinger");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 37,
                column: "ShortName",
                value: "Healthy");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 38,
                column: "ShortName",
                value: "Honor");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 39,
                column: "ShortName",
                value: "Iron Will");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 40,
                column: "ShortName",
                value: "Jury Rig");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 41,
                column: "ShortName",
                value: "Leadership");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 42,
                column: "ShortName",
                value: "Locksmith");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 43,
                column: "ShortName",
                value: "Marksman");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 44,
                column: "ShortName",
                value: "Martial Artist");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 45,
                column: "ShortName",
                value: "Mechanical Eng.");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 46,
                column: "ShortName",
                value: "Mountaineer");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 47,
                column: "ShortName",
                value: "Mule");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 48,
                column: "ShortName",
                value: "Pack Rat");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 49,
                column: "ShortName",
                value: "Pugulist");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 50,
                column: "ShortName",
                value: "Research");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 51,
                column: "ShortName",
                value: "Resourceful");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 52,
                column: "ShortName",
                value: "Salesmanship");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 53,
                column: "ShortName",
                value: "Search and Rescue");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 54,
                column: "ShortName",
                value: "Sharpshooter");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 55,
                column: "ShortName",
                value: "Stealthy");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 56,
                column: "ShortName",
                value: "Supp. Basic Skill");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 57,
                column: "ShortName",
                value: "Supp. Expert Skill");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 58,
                column: "ShortName",
                value: "Supp. Trained Skill");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 59,
                column: "ShortName",
                value: "Surgical Skills");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 60,
                column: "ShortName",
                value: "Survival Skills");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 61,
                column: "ShortName",
                value: "Swimmer");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 62,
                column: "ShortName",
                value: "Switch Hitter");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 63,
                column: "ShortName",
                value: "Teacher");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 64,
                column: "ShortName",
                value: "Therapist/SW");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 65,
                column: "ShortName",
                value: "Tough");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 66,
                column: "ShortName",
                value: "Traceur");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 67,
                column: "ShortName",
                value: "Trapper/Hunter");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 68,
                column: "ShortName",
                value: "Archery Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 69,
                column: "ShortName",
                value: "Kit/Tool Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 70,
                column: "ShortName",
                value: "Long Gun Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 71,
                column: "ShortName",
                value: "Melee Weapon Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 72,
                column: "ShortName",
                value: "Pistol Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 73,
                column: "ShortName",
                value: "Vehicle/Vessel Trn");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 74,
                column: "ShortName",
                value: "Trigger Discipline");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 75,
                column: "ShortName",
                value: "Volunteer");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 76,
                column: "ShortName",
                value: "Weekend Warrior");

            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 77,
                column: "ShortName",
                value: "Wheedle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "BaseAbilities");
        }
    }
}
