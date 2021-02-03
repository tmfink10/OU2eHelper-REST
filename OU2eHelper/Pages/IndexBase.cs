using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OU2eHelper.Components;
using OU2eHelper.Services;
using OU2eHelperModels.Models;

namespace OU2eHelper.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IBaseAbilityService BaseAbilityService { get; set; }
        public IEnumerable<BaseAbility> BaseAbilities { get; set; }

        [Inject]
        public IBaseAttributeService BaseAttributeService { get; set; }
        public IEnumerable<BaseAttribute> BaseAttributes { get; set; }

        [Inject] 
        public IBaseSkillService BaseSkillService { get; set; }
        public IEnumerable<BaseSkill> BaseSkills { get; set; }

        [Inject] 
        public IBaseTrainingValueService BaseTrainingValueService { get; set; }
        public IEnumerable<BaseTrainingValue> BaseTrainingValues { get; set; }

        [Inject]
        public IPlayerCharacterService PlayerCharacterService { get; set; }
        public IEnumerable<PlayerCharacter> PlayerCharacters { get; set; }

        [Inject]
        public IPlayerAbilityService PlayerAbilityService { get; set; }
        public IEnumerable<PlayerAbility> PlayerAbilities { get; set; }

        [Inject]
        public IPlayerSkillService PlayerSkillService { get; set; }
        public IEnumerable<PlayerSkill> PlayerSkills { get; set; }

        protected bool _createNew;
        protected bool _addAbilities;
        protected int InitialValue;
        protected int FinalValue;
        protected int Delta;
        [Parameter] public EventCallback<PlayerAbility> PlayerAbilityCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BaseAbilities = (await BaseAbilityService.GetBaseAbilities()).ToArray();
            BaseAttributes = (await BaseAttributeService.GetBaseAttributes()).ToArray();
            BaseSkills = (await BaseSkillService.GetBaseSkills()).ToArray();
            BaseTrainingValues = (await BaseTrainingValueService.GetTrainingValues()).ToArray();
            PlayerCharacters = (await PlayerCharacterService.GetPlayerCharacters()).ToArray();
        }

        public class HelperClass
        {
            public string FormString;
        }

        protected PlayerCharacter ThisCharacter = new PlayerCharacter();
        protected BaseAbility ThisBaseAbility = new BaseAbility();
        protected PlayerAbility ThisPlayerAbility = new PlayerAbility();
        protected PlayerSkill ThisPlayerSkill = new PlayerSkill();
        protected HelperClass Helper = new HelperClass();

        protected PlayerAttribute StrengthService { get; set; }
        protected PlayerAttribute PerceptionService { get; set; }
        protected PlayerAttribute EmpathyService { get; set; }
        protected PlayerAttribute WillpowerService { get; set; }

        protected void HandleNewCharacterClick()
        {
            _createNew = !_createNew;

            ThisCharacter.Age = 30;

            var strength = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Strength")
            };
            ThisCharacter.PlayerAttributes.Add(strength);
            StrengthService = strength;

            var perception = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Perception")
            };
            ThisCharacter.PlayerAttributes.Add(perception);
            PerceptionService = perception;

            var empathy = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Empathy")
            };
            ThisCharacter.PlayerAttributes.Add(empathy);
            EmpathyService = empathy;

            var willpower = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Willpower")
            };
            ThisCharacter.PlayerAttributes.Add(willpower);
            WillpowerService = willpower;

            ThisCharacter.DamageThreshold = strength.Bonus + willpower.Bonus;
            ThisCharacter.Morale = empathy.Bonus + willpower.Bonus;
            ThisCharacter.CargoCapacity = strength.Bonus;
            ThisCharacter.SurvivalPoints = 25;

            SetGestalt();
        }

        protected void InitializePlayerSkills(BaseSkill skill)
        {
            var playerSkill = new PlayerSkill { Value = 0, AdvancementsList = new List<int>()};
            if (skill.Type == "Expert")
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1 || skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2 || skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3 || skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4 || skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                }
            }
            else
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Value;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                }
            }

            playerSkill.BaseSkill = skill;

            ThisCharacter.PlayerSkills.Add(playerSkill);
        }
        
        protected async Task<PlayerCharacter> HandleOnValidPlayerCharacterSubmit()
        {
            //Sync the attribute services with the attributes in the list
            //Sync Strength
            var strength = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength");
            strength.Value = StrengthService.Value;
            strength.Points = StrengthService.Points;
            strength.Notes = StrengthService.Notes;
            //Sync Perception
            var perception = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception");
            perception.Value = PerceptionService.Value;
            perception.Points = PerceptionService.Points;
            perception.Notes = PerceptionService.Notes;
            //Sync Empathy
            var empathy = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy");
            empathy.Value = EmpathyService.Value;
            empathy.Points = EmpathyService.Points;
            empathy.Notes = EmpathyService.Notes;
            //Sync Willpower
            var willpower = ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower");
            willpower.Value = WillpowerService.Value;
            willpower.Points = WillpowerService.Points;
            willpower.Notes = WillpowerService.Notes;
            //Update secondary characteristics
            ThisCharacter.DamageThreshold = strength.Bonus + willpower.Bonus;
            ThisCharacter.Morale = empathy.Bonus + willpower.Bonus;
            ThisCharacter.CargoCapacity = strength.Bonus;

            _addAbilities = true;

            foreach (var skill in BaseSkills)
            {
                InitializePlayerSkills(skill);
            }

            if (ThisCharacter.Id != 0)
            {
                return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
            }
            else
            {
                return ThisCharacter = await PlayerCharacterService.CreatePlayerCharacter(ThisCharacter);
            }
        }

        protected async Task HandleOnValidBaseAbilitySubmit()
        {
            var tempAbility = new PlayerAbility
            {
                BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString))
            };

            if (tempAbility.BaseAbility.Description.Contains("Skill Support:"))
            {
                var end = tempAbility.BaseAbility.Description.IndexOf("}");

                var rawSkillNames = tempAbility.BaseAbility.Description.Remove(end);
                rawSkillNames = rawSkillNames.Remove(0, rawSkillNames.IndexOf("{") + 1);

                var SkillNames = new List<string>();
                var FinalList = new List<string>();

                while (rawSkillNames.Length > 0)
                {
                    Console.WriteLine($"RawSkillNames:{rawSkillNames}");
                    var tempString = rawSkillNames.Remove(rawSkillNames.IndexOf('%'));
                    SkillNames.Add(tempString);
                    tempString = "";
                    rawSkillNames = rawSkillNames.Remove(0, rawSkillNames.IndexOf('%') + 1);
                }

                foreach (var name in SkillNames)
                {
                    if (name.Contains(','))
                    {
                        FinalList.Add(name.Remove(0, 2));
                    }
                    else
                    {
                        FinalList.Add(name);
                    }
                }

                foreach (var name in FinalList)
                {
                    foreach (var skill in ThisCharacter.PlayerSkills)
                    {
                        if (skill.BaseSkill.Name == name)
                        {
                            skill.IsSupported = true;
                            tempAbility.SupportsPlayerSkills.Add(skill);
                        }

                        Console.WriteLine($"Search for: {name}\tIn:{skill.BaseSkill.Name}\tSupported: {skill.IsSupported}");
                    }
                }
            }

            tempAbility = await PlayerAbilityService.CreatePlayerAbility(tempAbility);
            
            ThisCharacter.PlayerAbilities.Add(tempAbility);
        }

        protected async Task<PlayerAbility> HandleOnValidPlayerAbilitySubmit()
        {
            Delta = FinalValue - InitialValue;
            UpdateGestalt();
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
        }

        protected async Task<PlayerAbility> HandleIncrementAbility(PlayerAbility ability)
        {
            if (ability.Tier > InitialValue)
            {
                ThisCharacter.GestaltLevel -= ability.Tier;
            }
            else if (ability.Tier < InitialValue)
            {
                ThisCharacter.GestaltLevel += (ability.Tier + 1);
            }

            InitialValue = ability.Tier;

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }

        protected async Task<PlayerSkill> HandleIncrementPlayerSkill(PlayerSkill skill)
        {
            if (skill.Value > InitialValue)
            {
                var supportValues = new List<int>();
                var totalAdvancement = 0;

                if (skill.BaseSkill.Type == "Basic")
                {
                    if (skill.IsSupported)
                    {
                        var roll = RollD5("Highest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    supportValues.Add(playerSkill.Advancements);
                                }
                            }
                        }
                        totalAdvancement += supportValues.Max();
                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        skill.Value += roll;
                        skill.AdvancementsList.Add(roll);
                    }
                }

                else if (skill.BaseSkill.Type == "Trained")
                {
                    if (skill.IsSpecialized)
                    {
                        var roll = RollD5("Highest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        if (skill.IsSupported)
                        {
                            foreach (var ability in ThisCharacter.PlayerAbilities)
                            {
                                foreach (var playerSkill in ability.SupportsPlayerSkills)
                                {
                                    if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                    {
                                        supportValues.Add(ability.Tier);
                                    }
                                }
                            }
                            totalAdvancement += supportValues.Max();
                        }
                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else if (skill.IsSupported)
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    supportValues.Add(ability.Tier);
                                }
                            }
                        }
                        totalAdvancement += supportValues.Max();
                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        var roll = RollD5("Lowest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        skill.Value += roll;
                        skill.AdvancementsList.Add(roll);
                    }
                }

                else if (skill.BaseSkill.Type == "Expert")
                {
                    if (skill.IsSpecialized)
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        if (skill.IsSupported)
                        {
                            foreach (var ability in ThisCharacter.PlayerAbilities)
                            {
                                foreach (var playerSkill in ability.SupportsPlayerSkills)
                                {
                                    if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                    {
                                        supportValues.Add(ability.Tier);
                                    }
                                }
                            }
                            totalAdvancement += supportValues.Max();
                        }

                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    if (skill.IsSupported)
                    {
                        var roll = RollD5("Lowest");
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        totalAdvancement += roll;
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            foreach (var playerSkill in ability.SupportsPlayerSkills)
                            {
                                if (playerSkill.BaseSkill.Name == skill.BaseSkill.Name)
                                {
                                    supportValues.Add(ability.Tier);
                                }
                            }
                        }
                        totalAdvancement += supportValues.Max();
                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        skill.Value = InitialValue;
                    }
                }
            }

            else if (skill.Value < InitialValue && skill.AdvancementsList.Count != 0)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value -= lastAdvancement;
                skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
            }

            else
            {
                skill.Value = InitialValue;
            }

            InitialValue = skill.Value;

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }

        protected int RollD5(string type = "Default")
        {
            var rand = new Random();
            var rolls = new List<int>();

            if (type == "Highest")
            {
                var roll1 = rand.Next(1, 5);
                rolls.Add(roll1);
                var roll2 = rand.Next(1, 5);
                rolls.Add(roll2);

                return rolls.Max();
            }

            if (type == "Lowest")
            {
                var roll1 = rand.Next(1, 5);
                rolls.Add(roll1);
                var roll2 = rand.Next(1, 5);
                rolls.Add(roll2);

                return rolls.Min();
            }

            return rand.Next(1, 5);
        }

        protected void SetGestalt()
        {
            if (ThisCharacter.Age < 36)
            {
                ThisCharacter.GestaltLevel = ThisCharacter.Age;
            }
            else
            {
                ThisCharacter.GestaltLevel = (ThisCharacter.Age - 35) / 5 + 35;
            }
        }

        protected void UpdateGestalt()
        {
            var positiveCounter = 0;
            var negativeCounter = 0;
            
            while (Delta>0)
            {
                InitialValue++;
                positiveCounter += InitialValue;
                Delta--;
            }

            while (Delta<0)
            {
                negativeCounter -= InitialValue;
                InitialValue--;
                Delta++;
            }

            if (positiveCounter > 0)
            {
                ThisCharacter.GestaltLevel -= positiveCounter;
            }
            else
            {
                ThisCharacter.GestaltLevel -= negativeCounter;
            }
            
        }

        protected void DeletePlayerAbility(PlayerAbility ability)
        {
            ThisCharacter.PlayerAbilities.Remove(ability);
            ThisCharacter.GestaltLevel = ThisCharacter.GestaltLevel + (((ability.Tier) * ability.Tier) / 2);
        }

        protected BSModal Step1Confirmation { get; set; }
        protected void onConfirmationToggle(MouseEventArgs e)
        {
            Step1Confirmation.Toggle();
        }

        protected BSModal StrengthDescription { get; set; }
        protected void onStrengthToggle(MouseEventArgs e)
        {
            StrengthDescription.Toggle();
        }

        protected BSModal PerceptionDescription { get; set; }
        protected void onPerceptionToggle(MouseEventArgs e)
        {
            PerceptionDescription.Toggle();
        }

        protected BSModal EmpathyDescription { get; set; }
        protected void onEmpathyToggle(MouseEventArgs e)
        {
            EmpathyDescription.Toggle();
        }

        protected BSModal WillpowerDescription { get; set; }
        protected void onWillpowerToggle(MouseEventArgs e)
        {
            WillpowerDescription.Toggle();
        }

        protected BSModal BaseAbilityDescription { get; set; }
        protected void onBaseAbilityToggleOn(BaseAbility ability)
        {
            ThisBaseAbility = ability;
            BaseAbilityDescription.Toggle();
        }
        protected void onBaseAbilityToggleOff()
        {
            BaseAbilityDescription.Toggle();
        }
    }
}
