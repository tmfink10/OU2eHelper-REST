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

        [Inject]
        public IPlayerAttributeService PlayerAttributeService { get; set; }
        public IEnumerable<PlayerAttribute> PlayerAttributes { get; set; }

        protected bool _createNew;
        protected bool _addAbilities;
        protected bool _addSkills;
        protected int InitialValue;
        protected int FinalValue;
        protected int Delta;

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
        protected PlayerAttribute ThisPlayerAttribute = new PlayerAttribute();
        protected PlayerSkill ThisPlayerSkill = new PlayerSkill();
        protected HelperClass Helper = new HelperClass();

        protected PlayerAttribute StrengthService { get; set; }
        protected PlayerAttribute PerceptionService { get; set; }
        protected PlayerAttribute EmpathyService { get; set; }
        protected PlayerAttribute WillpowerService { get; set; }

        protected async Task HandleNewCharacterClick()
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

            foreach (var skill in BaseSkills)
            {
                InitializePlayerSkills(skill);
            }

            SetGestalt();

            ThisCharacter = await PlayerCharacterService.CreatePlayerCharacter(ThisCharacter);
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

            return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
        }

        protected void InitializePlayerSkills(BaseSkill skill)
        {
            var playerSkill = new PlayerSkill { Value = 0, AdvancementsList = new List<int>()};
            if (skill.Type == "Expert")
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1 || skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                    playerSkill.AttributeValue += StrengthService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2 || skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                    playerSkill.AttributeValue += PerceptionService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3 || skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                    playerSkill.AttributeValue += EmpathyService.Bonus;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4 || skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                    playerSkill.AttributeValue += WillpowerService.Bonus;
                }
            }
            else
            {
                if (skill.PrimaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Value;
                    playerSkill.AttributeValue += StrengthService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Value;
                    playerSkill.AttributeValue += PerceptionService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Value;
                    playerSkill.AttributeValue += EmpathyService.Value;
                }

                if (skill.PrimaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Value;
                    playerSkill.AttributeValue += WillpowerService.Value;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 1)
                {
                    playerSkill.Value += StrengthService.Bonus;
                    playerSkill.AttributeValue += StrengthService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 2)
                {
                    playerSkill.Value += PerceptionService.Bonus;
                    playerSkill.AttributeValue += PerceptionService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 3)
                {
                    playerSkill.Value += EmpathyService.Bonus;
                    playerSkill.AttributeValue += EmpathyService.Bonus;
                }

                if (skill.SecondaryAttributeBaseAttributeId == 4)
                {
                    playerSkill.Value += WillpowerService.Bonus;
                    playerSkill.AttributeValue += WillpowerService.Bonus;
                }
            }

            playerSkill.BaseSkill = skill;

            ThisCharacter.PlayerSkills.Add(playerSkill);
        }

        protected async Task<PlayerSkill> HandleIncrementPlayerSkill(PlayerSkill skill)
        {
            if (skill.Advancements < 0)
            {
                skill.Advancements = 0;
                skill.Value = InitialValue;
            }
            if (skill.Value > InitialValue)
            {
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
                                    totalAdvancement += ability.Tier;
                                }
                            }
                        }
                        skill.Value = InitialValue + totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        var roll = RollD5();
                        ThisCharacter.GestaltLevel -= 1;
                        skill.Advancements += 1;
                        skill.Value = InitialValue + roll;
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
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
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
                                    totalAdvancement += ability.Tier;
                                }
                            }
                        }
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
                                        totalAdvancement += ability.Tier;
                                    }
                                }
                            }
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
                                    totalAdvancement += ability.Tier;
                                }
                            }
                        }
                        skill.Value += totalAdvancement;
                        skill.AdvancementsList.Add(totalAdvancement);
                    }
                    else
                    {
                        skill.Value = InitialValue;
                    }
                }

                if (skill.Advancements > 5)
                {
                    var lastAdvancement = skill.AdvancementsList[^1];
                    ThisCharacter.GestaltLevel += 1;
                    skill.Advancements -= 1;
                    skill.Value -= lastAdvancement;
                    skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
                }
            }

            else if (skill.Value < InitialValue && skill.AdvancementsList.Count != 0)
            {
                var lastAdvancement = skill.AdvancementsList[^1];
                ThisCharacter.GestaltLevel += 1;
                skill.Advancements -= 1;
                skill.Value = InitialValue - lastAdvancement;
                skill.AdvancementsList.Remove(skill.AdvancementsList[^1]);
            }

            else
            {
                skill.Value = InitialValue;
            }

            InitialValue = skill.Value;

            return await PlayerSkillService.UpdatePlayerSkill(skill.Id, skill);
        }
        
        protected async Task<PlayerAttribute> HandleIncrementPlayerAttribute(PlayerAttribute attribute)
        {
            var rand = new Random();
            var inListAttribute =
                ThisCharacter.PlayerAttributes.FirstOrDefault(a =>
                    a.BaseAttribute.Name == attribute.BaseAttribute.Name);

            if (InitialValue < attribute.Value)
            {
                var advanceValue = new int();
                advanceValue = rand.Next(1, 3);
                ThisCharacter.GestaltLevel -= InitialValue/10;
                attribute.Value += advanceValue;
                inListAttribute.Value = attribute.Value;
                attribute.AdvancementValues.Add(advanceValue);
                inListAttribute.AdvancementValues = attribute.AdvancementValues;
            }
            else if (InitialValue > attribute.Value)
            {
                if (attribute.AdvancementValues.Count > 0)
                {
                    attribute.Value -= attribute.AdvancementValues[^1];
                    inListAttribute.Value = attribute.Value;
                    attribute.AdvancementValues.Remove(attribute.AdvancementValues[^1]);
                    inListAttribute.AdvancementValues = attribute.AdvancementValues;
                    ThisCharacter.GestaltLevel += InitialValue/10;
                }
            }
            else
            {
                attribute.Value = InitialValue;
            }

            if (inListAttribute.Id == 0)
            {
                return await PlayerAttributeService.CreatePlayerAttribute(inListAttribute);
            }

            InitialValue = attribute.Value;
            return await PlayerAttributeService.UpdatePlayerAttribute(inListAttribute.Id, inListAttribute);
        }

        protected async Task HandleOnValidBaseAbilitySubmit()
        {
            var tempAbility = new PlayerAbility
            {
                BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString))
            };

            if (tempAbility.BaseAbility.Description.Contains("Skill Support: {"))
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

        protected void DeletePlayerAbility(PlayerAbility ability)
        {
            ThisCharacter.PlayerAbilities.Remove(ability);
            ThisCharacter.GestaltLevel = ThisCharacter.GestaltLevel + (((ability.Tier) * ability.Tier) / 2);
        }
        
        protected async Task<PlayerAbility> HandleIncrementPlayerAbility(PlayerAbility ability)
        {
            if (ability.Tier < 0)
            {
                if (InitialValue == 0)
                {
                    ability.Tier = 0;
                }
                if (InitialValue == 1)
                {
                    ThisCharacter.GestaltLevel += 1;
                    ability.Tier = 0;
                }
                if (InitialValue == 2)
                {
                    ThisCharacter.GestaltLevel += 3;
                    ability.Tier = 0;
                }
                if (InitialValue == 3)
                {
                    ThisCharacter.GestaltLevel += 6;
                    ability.Tier = 0;
                }
                if (InitialValue == 4)
                {
                    ThisCharacter.GestaltLevel += 10;
                    ability.Tier = 0;
                }
                if (InitialValue == 5)
                {
                    ThisCharacter.GestaltLevel += 15;
                    ability.Tier = 0;
                }
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            if (ability.Tier == 6)
            {
                ability.Tier = 5;
                return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
            }

            if (ability.Tier > 6)
            {
                if (InitialValue == 0)
                {
                    ability.Tier = 0;
                }
                else
                {
                    ThisCharacter.GestaltLevel += (((InitialValue - 1) * InitialValue) / 2) + InitialValue - 1;
                    ability.Tier = 0;
                }
            }
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

        protected int RollD5(string type = "Default")
        {
            var rand = new Random();
            var rolls = new List<int>();

            var roll1 = rand.Next(1, 6);
            if (roll1 == 6)
            {
                roll1 = 5;
            }
            rolls.Add(roll1);

            var roll2 = rand.Next(1, 6);
            if (roll2 == 6)
            {
                roll2 = 5;
            }
            rolls.Add(roll2);

            if (type == "Highest")
            {
                return rolls.Max();
            }

            if (type == "Lowest")
            {
               return rolls.Min();
            }

            return roll1;
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

        protected void HandleToggleSkills()
        {
            _addSkills = !_addSkills;
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
        
        protected BSModal WelcomeModal { get; set; }
        protected void ToggleWelcomeModal(MouseEventArgs e)
        {
            WelcomeModal.Toggle();
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
