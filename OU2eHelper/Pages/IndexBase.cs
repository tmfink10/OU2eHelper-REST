using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

        protected bool _createNew;
        protected bool _addAbilities;

        protected override async Task OnInitializedAsync()
        {
            BaseAbilities = (await BaseAbilityService.GetBaseAbilities()).ToArray();
            BaseAttributes = (await BaseAttributeService.GetBaseAttributes()).ToArray();
            BaseSkills = (await BaseSkillService.GetBaseSkills()).ToArray();
            BaseTrainingValues = (await BaseTrainingValueService.GetTrainingValues()).ToArray();
            PlayerCharacters = (await PlayerCharacterService.GetPlayerCharacters()).ToArray();
        }

        public PlayerAttribute Strength = new PlayerAttribute();
        public PlayerAttribute Perception = new PlayerAttribute();
        public PlayerAttribute Empathy = new PlayerAttribute();
        public PlayerAttribute Willpower = new PlayerAttribute();

        public class HelperClass
        {
            public string FormString;
        }
        protected PlayerCharacter ThisCharacter = new PlayerCharacter();
        protected BaseAbility ThisBaseAbility = new BaseAbility();
        protected PlayerAbility ThisPlayerAbility = new PlayerAbility();
        protected HelperClass Helper = new HelperClass();

        public void HandleNewCharacterClick()
        {
            _createNew = !_createNew;

            ThisCharacter.Age = 30;

            ThisCharacter.Strength = Strength;
            ThisCharacter.Strength.Value = 30;
            ThisCharacter.Strength.BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Strength");

            ThisCharacter.Perception = Perception;
            ThisCharacter.Perception.Value = 30;
            ThisCharacter.Perception.BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Perception");

            ThisCharacter.Empathy = Empathy;
            ThisCharacter.Empathy.Value = 30;
            ThisCharacter.Empathy.BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Empathy");

            ThisCharacter.Willpower = Willpower;
            ThisCharacter.Willpower.Value = 30;
            ThisCharacter.Willpower.BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Willpower");

            ThisCharacter.DamageThreshold = ThisCharacter.Strength.Bonus + ThisCharacter.Willpower.Bonus;
            ThisCharacter.Morale = ThisCharacter.Empathy.Bonus + ThisCharacter.Willpower.Bonus;
            ThisCharacter.CargoCapacity = ThisCharacter.Strength.Bonus;

            SetGestalt();
        }

        public async Task HandleOnValidSubmit()
        {
            if (PlayerCharacters.FirstOrDefault(c => c.Id == ThisCharacter.Id) != null)
            {
                await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter);
            }
            else
            {
                await PlayerCharacterService.CreatePlayerCharacter(ThisCharacter);
            }
        }

        public void HandleOnValidAbilitySubmit()
        {
            Console.WriteLine($"{Helper.FormString}");
            var tempAbility = new PlayerAbility();
            tempAbility.BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString));
            ThisCharacter.Abilities.Add(tempAbility);
        }

        public void SetGestalt()
        {
            if (ThisCharacter.Age < 36)
            {
                ThisCharacter.GestaltLevel = ThisCharacter.Age;
            }
            else
            {
                ThisCharacter.GestaltLevel = (ThisCharacter.Age - 35) / 5 + 35;
            }

            ThisCharacter.SurvivalPoints = 25;
        }

        protected BSModal Confirmation { get; set; }
        protected void onConfirmationToggle(MouseEventArgs e)
        {
            Confirmation.Toggle();
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

        protected void HandleAbilitiesClick()
        {
            _addAbilities = !_addAbilities;
        }

        protected void IncrementStrength()
        {

        }
    }
}
