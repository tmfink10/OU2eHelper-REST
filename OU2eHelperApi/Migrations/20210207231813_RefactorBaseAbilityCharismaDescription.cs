using Microsoft.EntityFrameworkCore.Migrations;

namespace OU2eHelperApi.Migrations
{
    public partial class RefactorBaseAbilityCharismaDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. \r\n\r\nSpecial Feature(s): \r\nEach Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.\r\n\r\nA character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.\r\n\r\nSkill Support: {Diplomacy <Barter/Bribe>%, Diplomacy <Command>%, Diplomacy <Determine Motives>%, Diplomacy <Persuade>%, Expression%}.\r\n\r\nMastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold.", "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. <br/><br/>Special Feature(s): <br/>Each Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.<br/><br/>A character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.<br/><br/>Skill Support: {Diplomacy <Barter/Bribe>%, Diplomacy <Command>%, Diplomacy <Determine Motives>%, Diplomacy <Persuade>%, Expression%}.<br/><br/>Mastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BaseAbilities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "HtmlDescription" },
                values: new object[] { "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. \r\n\r\nSpecial Feature(s): \r\nEach Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.\r\n\r\nA character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.\r\n\r\nSkill Support: {Diplomacy <any but Intimidate>, Expression%}.\r\n\r\nMastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold.", "Call it stage presence, charm, wit, bedside manner or any combination thereof; this character has an uncommon charm about their person that makes them well liked, personable, and (seemingly) trustworthy. <br/><br/>Special Feature(s): <br/>Each Tier allows a character to add 1 to Morale whenever they are in a Formation where they are the “Point” position.<br/><br/>A character with this Ability will be sorely missed should anything happen to them. Any Checks made to resist the loss of Morale due to this character’s death or peril will get 1 Difficulty Die per Tier.<br/><br/>Skill Support: {Diplomacy <any but Intimidate>, Expression%}.<br/><br/>Mastery: This character can substitute any Wb with Eb. Also, this character will prevent the addition of 1 Governance per Eb when utilizing Mobilized Workforce around a Stronghold." });
        }
    }
}
