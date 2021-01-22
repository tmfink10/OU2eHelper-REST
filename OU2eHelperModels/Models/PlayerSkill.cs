using System;
using System.Collections.Generic;
using System.Text;

namespace OU2eHelperModels.Models
{
    public class PlayerSkill
    {
        public int Id { get; set; }
        //public PlayerCharacter PlayerCharacter { get; set; }
        public BaseSkill BaseSkill { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public int Advancements { get; set; }
        public bool Supported { get; set; }
    }
}
