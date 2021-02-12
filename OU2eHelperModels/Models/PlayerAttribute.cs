using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OU2eHelperModels.Models
{
    public class PlayerAttribute
    {
        public int Id { get; set; }
        public int PlayerCharacterId { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public BaseAttribute BaseAttribute { get; set; }
        public List<int> AdvancementValues { get; set; } = new List<int>();
        public int Value { get; set; } = 0;
        public int Points { get; set; } = 0;
        public int Bonus => Value / 10;
        public string Notes { get; set; } = "";
    }
}
