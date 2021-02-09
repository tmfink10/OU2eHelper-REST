using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.util;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorStrap;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
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
        protected string AbilityAttributeSelectionValue;

        protected override async Task OnInitializedAsync()
        {
            BaseAbilities = (await BaseAbilityService.GetBaseAbilities()).ToArray();
            BaseAttributes = (await BaseAttributeService.GetBaseAttributes()).ToArray();
            BaseSkills = (await BaseSkillService.GetBaseSkills()).ToArray();
            BaseTrainingValues = (await BaseTrainingValueService.GetTrainingValues()).ToArray();
            PlayerCharacters = (await PlayerCharacterService.GetPlayerCharacters()).ToArray();

            foreach (var ability in BaseAbilities)
            {
                if (ability.ModifiesTrainingValuesCoded != null)
                {
                    var searchValues = CsvStringToArray(ability.ModifiesTrainingValuesCoded);

                    foreach (var value in searchValues)
                    {
                        ability.ModifiesBaseTrainingValues.Add(BaseTrainingValues.FirstOrDefault(t => t.Name == value.ToString()));
                    }

                    foreach (var item in ability.ModifiesBaseTrainingValues)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
            }

        }

        protected Array CsvStringToArray(string values)
        {
            return values.Split(',');
        }

        public class HelperClass
        {
            public string FormString;
            public string FormString2;
        }

        protected PlayerCharacter ThisCharacter = new PlayerCharacter();
        protected BaseAbility ThisBaseAbility = new BaseAbility();
        protected BaseSkill ThisBaseSkill = new BaseSkill();
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
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Strength"), Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(strength);
            StrengthService = strength;

            var perception = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Perception"), Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(perception);
            PerceptionService = perception;

            var empathy = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Empathy"), Points = 3
            };
            ThisCharacter.PlayerAttributes.Add(empathy);
            EmpathyService = empathy;

            var willpower = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Willpower"), Points = 3
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

            foreach (var trainingValue in BaseTrainingValues)
            {
                InitializePlayerTrainingValues(trainingValue);
            }

            SetGestalt();

            foreach (var ability in BaseAbilities)
            {
                if (ability.UsesBaseAttributesCoded.Contains("S"))
                {
                    ability.UsesBaseAttributes.Add(StrengthService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("P"))
                {
                    ability.UsesBaseAttributes.Add(PerceptionService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("E"))
                {
                    ability.UsesBaseAttributes.Add(EmpathyService.BaseAttribute);
                }

                if (ability.UsesBaseAttributesCoded.Contains("W"))
                {
                    ability.UsesBaseAttributes.Add(WillpowerService.BaseAttribute);
                }
            }

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

        protected async Task<PlayerAttribute> HandleIncrementPlayerAttribute(PlayerAttribute attribute)
        {
            var rand = new Random();
            var inListAttribute =
                ThisCharacter.PlayerAttributes.FirstOrDefault(a =>
                    a.BaseAttribute.Name == attribute.BaseAttribute.Name);
            var valueToRemove = 0;

            if (InitialValue < attribute.Value)
            {
                var advanceValue = 0;
                advanceValue = rand.Next(1, 4);
                ThisCharacter.GestaltLevel -= InitialValue/10;
                attribute.Value = InitialValue + advanceValue;
                inListAttribute.Value = attribute.Value;
                attribute.AdvancementValues.Add(advanceValue);
                inListAttribute.AdvancementValues = attribute.AdvancementValues;
                if (attribute.Points < attribute.Bonus)
                {
                    attribute.Points = attribute.Bonus;
                    inListAttribute.Points = inListAttribute.Bonus;
                    if (ThisCharacter.PlayerAbilities != null)
                    {
                        foreach (var ability in ThisCharacter.PlayerAbilities)
                        {
                            if (ability.AddedUsingBaseAttributeCode == attribute.BaseAttribute.Name)
                            {
                                attribute.Points -= 1;
                                inListAttribute.Points -= 1;
                            }
                        }
                    }
                    
                }
            }
            else if (InitialValue > attribute.Value)
            {
                if (attribute.AdvancementValues.Count > 0)
                {
                    valueToRemove = attribute.AdvancementValues[^1];
                    attribute.Value = InitialValue - valueToRemove;
                    inListAttribute.Value = attribute.Value;
                    attribute.AdvancementValues.Remove(attribute.AdvancementValues[^1]);
                    inListAttribute.AdvancementValues = attribute.AdvancementValues;
                    ThisCharacter.GestaltLevel += attribute.Bonus;
                }
                if (attribute.Bonus < InitialValue / 10)
                {
                    attribute.Points -= 1;
                    inListAttribute.Points -= 1;
                }

            }
            else
            {
                attribute.Value = InitialValue;
                inListAttribute.Value = InitialValue;
            }

            if (inListAttribute.Id == 0)
            {
                return await PlayerAttributeService.CreatePlayerAttribute(inListAttribute);
            }

            foreach (var skill in ThisCharacter.PlayerSkills)
            {
                var primaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.PrimaryAttributeBaseAttributeId);
                var secondaryAttribute = BaseAttributes.FirstOrDefault(a => a.Id == skill.BaseSkill.SecondaryAttributeBaseAttributeId);

                if (primaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (skill.BaseSkill.Type == "Basic" || skill.BaseSkill.Type == "Trained")
                    {
                        if (InitialValue < attribute.Value)
                        {
                            skill.Value += attribute.AdvancementValues[^1];
                        }
                        else
                        {
                            skill.Value -= valueToRemove;
                        }
                    }

                    else if (skill.BaseSkill.Type == "Expert")
                    {
                        if (InitialValue/10 > attribute.Value/10)
                        {
                            skill.Value -= 1;
                        }
                        else if (InitialValue/10 < attribute.Value/10)
                        {
                            skill.Value += 1;
                        }
                    }
                }
                else if (secondaryAttribute.Name == attribute.BaseAttribute.Name)
                {
                    if (InitialValue / 10 > attribute.Value / 10)
                    {
                        skill.Value -= 1;
                    }
                    else if (InitialValue / 10 < attribute.Value / 10)
                    {
                        skill.Value += 1;
                    }
                }
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

            if (tempAbility.AddedUsingBaseAttributeCode == null)
            {
                if (tempAbility.BaseAbility.UsesBaseAttributes.Count == 1)
                {
                    tempAbility.AddedUsingBaseAttributeCode = tempAbility.BaseAbility.UsesBaseAttributes[0].Name;
                    ThisCharacter.PlayerAttributes
                        .FirstOrDefault(a => a.BaseAttribute.Name == tempAbility.AddedUsingBaseAttributeCode).Points -= 1;
                }
                else
                {
                    onPlayerAbilityToggleOn(tempAbility);
                }
            }

            ThisCharacter.PlayerAbilities.Add(tempAbility);

        }

        protected async Task<PlayerAbility> HandleOnValidPlayerAbilitySubmit()
        {
            Delta = FinalValue - InitialValue;
            UpdateGestalt();
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
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
                if (ThisCharacter.TrainingValues != null)
                {
                    foreach (var trainingValue in ThisCharacter.TrainingValues)
                    {
                        foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                        {
                            if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                            {
                                trainingValue.Value += 1;
                            }
                        }
                    }
                }
            }
            else if (ability.Tier < InitialValue)
            {
                ThisCharacter.GestaltLevel += (ability.Tier + 1);
                if (ThisCharacter.TrainingValues != null)
                {
                    foreach (var trainingValue in ThisCharacter.TrainingValues)
                    {
                        foreach (var baseTrainingValue in ability.BaseAbility.ModifiesBaseTrainingValues)
                        {
                            if (trainingValue.BaseTrainingValue.Name == baseTrainingValue.Name)
                            {
                                trainingValue.Value -= 1;
                            }
                        }
                    }
                }
            }

            InitialValue = ability.Tier;

            return await PlayerAbilityService.UpdatePlayerAbility(ability.Id, ability);
        }
        
        protected void DeletePlayerAbility(PlayerAbility ability)
        {
            ThisCharacter.PlayerAbilities.Remove(ability);
            ThisCharacter.GestaltLevel = ThisCharacter.GestaltLevel + (((ability.Tier-1) * ability.Tier) / 2) + ability.Tier;
            ThisCharacter.PlayerAttributes
                .FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode).Points += 1;
        }

        protected void InitializePlayerSkills(BaseSkill skill)
        {
            var playerSkill = new PlayerSkill { Value = 0, AdvancementsList = new List<int>() };
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
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
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
                                        if (ability.BaseAbility.AdvancesSkills)
                                        {
                                            totalAdvancement += ability.Tier;
                                        }
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
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
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
                                        if (ability.BaseAbility.AdvancesSkills)
                                        {
                                            totalAdvancement += ability.Tier;
                                        }
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
                                    if (ability.BaseAbility.AdvancesSkills)
                                    {
                                        totalAdvancement += ability.Tier;
                                    }
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

        protected void InitializePlayerTrainingValues(BaseTrainingValue value)
        {
            var tempPlayerTrainingValue = new PlayerTrainingValue {BaseTrainingValue = value, Value = 0};
            ThisCharacter.TrainingValues.Add(tempPlayerTrainingValue);
        }
        
        protected int RollD5(string type = "Default")
        {
            var rand = new Random();
            var rolls = new List<int>();

            var roll1 = rand.Next(1, 7);
            if (roll1 == 6)
            {
                roll1 = 5;
            }
            rolls.Add(roll1);

            var roll2 = rand.Next(1, 7);
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

        protected BSModal GestaltDescription { get; set; }
        protected void ToggleGestaltDescription(MouseEventArgs e)
        {
            GestaltDescription.Toggle();
        }

        protected BSModal SurvivalPointsDescription { get; set; }
        protected void ToggleSurvivalPointsDescription(MouseEventArgs e)
        {
            SurvivalPointsDescription.Toggle();
        }

        protected BSModal CompetencePointsDescription { get; set; }
        protected void ToggleCompetencePointsDescription(MouseEventArgs e)
        {
            CompetencePointsDescription.Toggle();
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

        protected BSModal PlayerAbilityAttributeSelection { get; set; }
        protected void onPlayerAbilityToggleOn(PlayerAbility ability)
        {
            ThisPlayerAbility = ability;
            PlayerAbilityAttributeSelection.Toggle();
        }
        protected void onPlayerAbilityToggleOff(PlayerAbility ability)
        {
            ThisCharacter.PlayerAttributes
                .FirstOrDefault(a => a.BaseAttribute.Name == ability.AddedUsingBaseAttributeCode).Points -= 1;
            PlayerAbilityAttributeSelection.Toggle();
        }

        protected async Task<PlayerAbility> UpdateThisPlayerAttribute()
        {
            return await PlayerAbilityService.UpdatePlayerAbility(ThisPlayerAbility.Id, ThisPlayerAbility);
        }

        protected BSModal BaseSkillDescription { get; set; }
        protected void onBaseSkillToggleOn(BaseSkill skill)
        {
            ThisBaseSkill = skill;
            BaseSkillDescription.Toggle();
        }
        protected void onBaseSkillToggleOff()
        {
            BaseSkillDescription.Toggle();
        }

        protected void GeneratePdf()
        {
            var spewFontSize = 20;
            var headerFontSize = 13;
            var resourcesFontSize = 12;
            var skillsFontSize = 10;
            var trainingValueFontSize = 12;
            var skillIndentLeft = 151;
            var skillIndentRight = 302;
            var trainingValueIndentTopLeft = 438;
            var trainingValueIndentTopRight = 519;
            var trainingValueIndentBottomLeft = 399;
            var trainingValueIndentBottomMiddle = 479;
            var trainingValueIndentBottomRight = 559;

            Label name = new Label(ThisCharacter.FullName, 100, 30, 504, 100, Font.Helvetica, headerFontSize, TextAlign.Left);

            Label strengthBonus = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Value.ToString().Substring(0,1), 70, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label strengthTens = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Value.ToString().Substring(1, 1), 94, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label perceptionBonus = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Value.ToString().Substring(0, 1), 128, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label perceptionTens = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Value.ToString().Substring(1, 1), 152, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label empathyBonus = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Value.ToString().Substring(0, 1), 187, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label empathyTens = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Value.ToString().Substring(1, 1), 211, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label willpowerBonus = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Value.ToString().Substring(0, 1), 245, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);
            Label willpowerTens = new Label(ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Value.ToString().Substring(1, 1), 269, 175, 504, 100, Font.Helvetica, spewFontSize, TextAlign.Left);

            Label survivalPoints = new Label(ThisCharacter.SurvivalPoints.ToString(), 104, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left);
            Label gestaltLevel = new Label(ThisCharacter.GestaltLevel.ToString(), 193, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left);
            Label competencePoints = new Label(ThisCharacter.CompetencePoints.ToString(), 277, 224, 504, 100, Font.Helvetica, resourcesFontSize, TextAlign.Left);

            Label balance = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Balance").Value.ToString(), skillIndentLeft, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label brawl = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Brawl").Value.ToString(), skillIndentLeft, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label climb = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Climb").Value.ToString(), skillIndentLeft, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label composure = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Composure").Value.ToString(), skillIndentLeft, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label dodge = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Dodge").Value.ToString(), skillIndentLeft, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label endurance = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Endurance").Value.ToString(), skillIndentLeft, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label expression = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Expression").Value.ToString(), skillIndentLeft, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label grapple = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Grapple").Value.ToString(), skillIndentLeft, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label bow = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Bow/Crossbow").Value.ToString(), skillIndentLeft, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label calmOther = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Calm Other").Value.ToString(), skillIndentLeft, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label diplomacy = new Label("", skillIndentLeft, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label barter = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Diplomacy <Barter/Bribe>").Value.ToString(), skillIndentLeft, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label command = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Diplomacy <Command>").Value.ToString(), skillIndentLeft, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label determineMotives = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Diplomacy <Determine Motives>").Value.ToString(), skillIndentLeft, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label intimidate = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Diplomacy <Intimidate>").Value.ToString(), skillIndentLeft, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label persuade = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Diplomacy <Persuade>").Value.ToString(), skillIndentLeft, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label digitalSystems = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Digital Systems").Value.ToString(), skillIndentLeft, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label advancedMedicine = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Advanced Medicine").Value.ToString(), skillIndentLeft, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label constructionEngineering = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Craft/Construct/Engineer").Value.ToString(), skillIndentLeft, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s1 = new Label("", skillIndentLeft, 633, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s2 = new Label("", skillIndentLeft, 649, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s3 = new Label("", skillIndentLeft, 665, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s4 = new Label("", skillIndentLeft, 681, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label martialArts = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Martial Arts").Value.ToString(), skillIndentLeft, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label pilot = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Pilot").Value.ToString(), skillIndentLeft, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s5 = new Label("", skillIndentLeft, 729, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s6 = new Label("", skillIndentLeft, 745, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);

            Label hold = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Hold").Value.ToString(), skillIndentRight, 291, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label jumpLeap = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Jump/Leap").Value.ToString(), skillIndentRight, 307, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label liftPull = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Lift/Pull").Value.ToString(), skillIndentRight, 323, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label resistPain = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Resist Pain").Value.ToString(), skillIndentRight, 339, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label search = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Search").Value.ToString(), skillIndentRight, 355, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label spotListen = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Spot/Listen").Value.ToString(), skillIndentRight, 371, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label stealth = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Stealth").Value.ToString(), skillIndentRight, 387, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label empty = new Label("", skillIndentRight, 403, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label longGun = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Firearms <Long Gun>").Value.ToString(), skillIndentRight, 439, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label pistol = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Firearms <Pistol>").Value.ToString(), skillIndentRight, 455, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label firstAid = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "First Aid").Value.ToString(), skillIndentRight, 471, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label bludgeon = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Melee Attack <Bludgeoning>").Value.ToString(), skillIndentRight, 487, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label pierce = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Melee Attack <Piercing>").Value.ToString(), skillIndentRight, 503, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label slash = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Melee Attack <Slashing>").Value.ToString(), skillIndentRight, 519, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label navigation = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Navigation").Value.ToString(), skillIndentRight, 535, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label swim = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Swim").Value.ToString(), skillIndentRight, 550, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label thrw = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Throw").Value.ToString(), skillIndentRight, 566, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label ride = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Ride").Value.ToString(), skillIndentRight, 601, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s7 = new Label("", skillIndentRight, 617, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label science = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Science").Value.ToString(), skillIndentRight, 633, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s8 = new Label("", skillIndentRight, 649, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s9 = new Label("", skillIndentRight, 665, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label survival = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Survival").Value.ToString(), skillIndentRight, 681, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s10 = new Label("", skillIndentRight, 697, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s11 = new Label("", skillIndentRight, 713, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label s12 = new Label("", skillIndentRight, 729, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label toughness = new Label(ThisCharacter.PlayerSkills.FirstOrDefault(s => s.BaseSkill.Name == "Toughness").Value.ToString(), skillIndentRight, 745, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);

            Label damageThreshold = new Label(ThisCharacter.DamageThreshold.ToString(), 520, 132, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);
            Label psyche = new Label(ThisCharacter.Morale.ToString(), 445, 313, 504, 100, Font.Helvetica, skillsFontSize, TextAlign.Left);

            Label archeryGearTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Archery Gear").Value.ToString(), trainingValueIndentTopLeft, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label bludgeonTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Bludgeon").Value.ToString(), trainingValueIndentTopLeft, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label pierceTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Piercing").Value.ToString(), trainingValueIndentTopLeft, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label slashTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Slashing").Value.ToString(), trainingValueIndentTopLeft, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);

            Label longGunTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Long Gun").Value.ToString(), trainingValueIndentTopRight, 450, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label pistolTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Pistol").Value.ToString(), trainingValueIndentTopRight, 490, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label throwingTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Throwing").Value.ToString(), trainingValueIndentTopRight, 531, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label martialArtsTV = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Martial Arts").Value.ToString(), trainingValueIndentTopRight, 571, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);

            Label athleticGear = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Athletic Gear").Value.ToString(), trainingValueIndentBottomLeft, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label climbingGear = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Climbing Gear").Value.ToString(), trainingValueIndentBottomLeft, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label commandApp = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Command Apparatus").Value.ToString(), trainingValueIndentBottomLeft, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label firefighting = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Firefighting").Value.ToString(), trainingValueIndentBottomLeft, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);

            Label firstAidKit = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "First Aid Kit").Value.ToString(), trainingValueIndentBottomMiddle, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label medicalGear = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Medical Gear").Value.ToString(), trainingValueIndentBottomMiddle, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label reconGear = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Reconnaissance Gear").Value.ToString(), trainingValueIndentBottomMiddle, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label survivalKit = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Survival Kit").Value.ToString(), trainingValueIndentBottomMiddle, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);

            Label swimmingDiving = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Swimming/Diving").Value.ToString(), trainingValueIndentBottomRight, 614, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label tools = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Tools").Value.ToString(), trainingValueIndentBottomRight, 655, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label value = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Value").Value.ToString(), trainingValueIndentBottomRight, 695, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);
            Label vehicles = new Label(ThisCharacter.TrainingValues.FirstOrDefault(t => t.BaseTrainingValue.Name == "Vehicles").Value.ToString(), trainingValueIndentBottomRight, 735, 504, 100, Font.Helvetica, trainingValueFontSize, TextAlign.Left);

            MergeDocument mergeDoc = new MergeDocument(GetPath("Resources/OU2eCharacterSheet.pdf"));

            Page page0 = mergeDoc.Pages[0];

            page0.Elements.Add(name);
            page0.Elements.Add(strengthBonus);
            page0.Elements.Add(strengthTens);
            page0.Elements.Add(perceptionBonus);
            page0.Elements.Add(perceptionTens);
            page0.Elements.Add(empathyBonus);
            page0.Elements.Add(empathyTens);
            page0.Elements.Add(willpowerBonus);
            page0.Elements.Add(willpowerTens);
            page0.Elements.Add(survivalPoints);
            page0.Elements.Add(gestaltLevel);
            page0.Elements.Add(competencePoints);
            page0.Elements.Add(balance);
            page0.Elements.Add(brawl);
            page0.Elements.Add(climb);
            page0.Elements.Add(composure);
            page0.Elements.Add(dodge);
            page0.Elements.Add(endurance);
            page0.Elements.Add(expression);
            page0.Elements.Add(grapple);
            page0.Elements.Add(bow);
            page0.Elements.Add(calmOther);
            page0.Elements.Add(barter);
            page0.Elements.Add(command);
            page0.Elements.Add(determineMotives);
            page0.Elements.Add(intimidate);
            page0.Elements.Add(persuade);
            page0.Elements.Add(digitalSystems);
            page0.Elements.Add(advancedMedicine);
            page0.Elements.Add(constructionEngineering);
            page0.Elements.Add(martialArts);
            page0.Elements.Add(pilot);
            page0.Elements.Add(hold);
            page0.Elements.Add(jumpLeap);
            page0.Elements.Add(liftPull);
            page0.Elements.Add(resistPain);
            page0.Elements.Add(search);
            page0.Elements.Add(spotListen);
            page0.Elements.Add(stealth);
            page0.Elements.Add(longGun);
            page0.Elements.Add(pistol);
            page0.Elements.Add(firstAid);
            page0.Elements.Add(bludgeon);
            page0.Elements.Add(pierce);
            page0.Elements.Add(slash);
            page0.Elements.Add(navigation);
            page0.Elements.Add(swim);
            page0.Elements.Add(thrw);
            page0.Elements.Add(ride);
            page0.Elements.Add(science);
            page0.Elements.Add(survival);
            page0.Elements.Add(toughness);
            page0.Elements.Add(damageThreshold);
            page0.Elements.Add(psyche);
            page0.Elements.Add(archeryGearTV);
            page0.Elements.Add(bludgeonTV);
            page0.Elements.Add(pierceTV);
            page0.Elements.Add(slashTV);
            page0.Elements.Add(longGunTV);
            page0.Elements.Add(pistolTV);
            page0.Elements.Add(throwingTV);
            page0.Elements.Add(martialArtsTV);
            page0.Elements.Add(athleticGear);
            page0.Elements.Add(climbingGear);
            page0.Elements.Add(commandApp);
            page0.Elements.Add(firefighting);
            page0.Elements.Add(firstAidKit);
            page0.Elements.Add(medicalGear);
            page0.Elements.Add(reconGear);
            page0.Elements.Add(survivalKit);
            page0.Elements.Add(swimmingDiving);
            page0.Elements.Add(tools);
            page0.Elements.Add(value);
            page0.Elements.Add(vehicles);

            mergeDoc.Draw(@"G:\Dump\Output.pdf");
        }

        internal static string GetPath(string filePath)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, filePath);
        }
    }
}
