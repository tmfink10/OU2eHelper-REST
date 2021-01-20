using System;
using System.Collections.Generic;
using System.Text;

namespace OU2eHelperModels.Models
{
    public class PlayerCharacter
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public string Sex { get; set; }
        public Dictionary<string, PlayerAttribute> Attributes { get; set; } = new Dictionary<string, PlayerAttribute>();
        public PlayerAttribute Strength { get; set; }
        public PlayerAttribute Perception { get; set; }
        public PlayerAttribute Empathy { get; set; }
        public PlayerAttribute Willpower { get; set; }
        public List<PlayerSkill> Skills { get; set; } = new List<PlayerSkill>();
        public List<PlayerAbility> Abilities { get; set; } = new List<PlayerAbility>();
        public List<PlayerTrainingValue> TrainingValues { get; set; } = new List<PlayerTrainingValue>();
        public int SurvivalPoints { get; set; } = 0;
        public int GestaltLevel { get; set; } = 0;
        public int CargoCapacity { get; set; } = 0;
        public int CompetencePoints { get; set; } = 0;
        public int HealthPoints { get; set; } = 0;
        public int DamageThreshold { get; set; } = 0;
        public int Morale { get; set; } = 0;
        public string Notes { get; set; } = "";

        //private PlayerCharacter()
        //{
        //    //Constructor for EntityFramework Core
        //}


        public PlayerCharacter()
        {

        }
        //public PlayerCharacter(PlayerAttribute strength, PlayerAttribute perception, PlayerAttribute empathy, PlayerAttribute willpower)
        //{
        //    Strength = strength;
        //    Perception = perception;
        //    Empathy = empathy;
        //    Willpower = willpower;
        //    Attributes.Add("Strength", Strength);
        //    Attributes.Add("Perception", Perception);
        //    Attributes.Add("Empathy", Empathy);
        //    Attributes.Add("Willpower", Willpower);

        //}

        public int CheckSkill(PlayerSkill skill)
        {
            var rnd = new Random();

            PlayerAttribute primaryAttribute;

            Attributes.TryGetValue(skill.BaseSkill.PrimaryAttribute.Name, out primaryAttribute);

            PlayerAttribute secondaryAttribute;
            Attributes.TryGetValue(skill.BaseSkill.SecondaryAttribute.Name, out secondaryAttribute);

            var checkValue = primaryAttribute.Value + secondaryAttribute.Bonus + skill.Value;

            var roll = rnd.Next(1, 100);


            Console.WriteLine($"Primary Attribute Value: {primaryAttribute.Value}");
            Console.WriteLine($"Secondary Attribute Value: {secondaryAttribute.Bonus}");
            Console.WriteLine($"Skill Value: {skill.Value}");
            Console.WriteLine($"Check Value: {checkValue}");
            Console.WriteLine($"Roll: {roll}");

            return checkValue - roll;
        }
    }
}
