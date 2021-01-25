using System;
using System.Collections.Generic;
using System.Text;

namespace OU2eHelperModels.Models
{
    public class PlayerAttribute
    {
        public int Id { get; set; }
        public int PlayerCharacterId { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public BaseAttribute BaseAttribute { get; set; }
        public int Value { get; set; } = 0;
        public int Points { get; set; } = 0;
        public int Bonus => Value / 10;
        public string Notes { get; set; } = "";

        public void Advance(PlayerCharacter character)
        {
            var rand = new Random();
            Value += rand.Next(1, 3);
            character.GestaltLevel -= Bonus;
        }
    }
}
