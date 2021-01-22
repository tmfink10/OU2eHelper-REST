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

            var strength = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Strength")
            };
            ThisCharacter.PlayerAttributes.Add(strength);
            ThisCharacter.Attributes.Add(strength.BaseAttribute.Name, strength);
            ThisCharacter.StrengthService = strength;

            var perception = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Perception")
            };
            ThisCharacter.PlayerAttributes.Add(perception);
            ThisCharacter.Attributes.Add(perception.BaseAttribute.Name, perception);
            ThisCharacter.PerceptionService = perception;

            var empathy = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Empathy")
            };
            ThisCharacter.PlayerAttributes.Add(empathy);
            ThisCharacter.Attributes.Add(empathy.BaseAttribute.Name, empathy);
            ThisCharacter.EmpathyService = empathy;

            var willpower = new PlayerAttribute
            {
                Value = 30, BaseAttribute = BaseAttributes.FirstOrDefault(a => a.Name == "Willpower")
            };
            ThisCharacter.PlayerAttributes.Add(willpower);
            ThisCharacter.Attributes.Add(willpower.BaseAttribute.Name, willpower);
            ThisCharacter.WillpowerService = willpower;

            ThisCharacter.DamageThreshold = strength.Bonus + willpower.Bonus;
            ThisCharacter.Morale = empathy.Bonus + willpower.Bonus;
            ThisCharacter.CargoCapacity = strength.Bonus;
            ThisCharacter.SurvivalPoints = 25;

            SetGestalt();
        }

        public async Task<PlayerCharacter> HandleOnValidSubmit()
        {
            if (ThisCharacter.Id != 0)
            {
                //Sync the attribute services with the attributes in the list
                //Sync Strength
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Value =
                        ThisCharacter.StrengthService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Points =
                    ThisCharacter.StrengthService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Notes =
                    ThisCharacter.StrengthService.Notes;
                //Sync Perception
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Value =
                    ThisCharacter.PerceptionService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Points =
                    ThisCharacter.PerceptionService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Notes =
                    ThisCharacter.PerceptionService.Notes;
                //Sync Empathy
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Value =
                    ThisCharacter.EmpathyService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Points =
                    ThisCharacter.EmpathyService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Notes =
                    ThisCharacter.EmpathyService.Notes;
                //Sync Willpower
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Value =
                    ThisCharacter.WillpowerService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Points =
                    ThisCharacter.WillpowerService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Notes =
                    ThisCharacter.WillpowerService.Notes;

                return await PlayerCharacterService.UpdatePlayerCharacter(ThisCharacter.Id, ThisCharacter);
            }
            else
            {
                //Sync the attribute services with the attributes in the list
                //Sync Strength
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Value =
                        ThisCharacter.StrengthService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Points =
                    ThisCharacter.StrengthService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Strength").Notes =
                    ThisCharacter.StrengthService.Notes;
                //Sync Perception
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Value =
                    ThisCharacter.PerceptionService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Points =
                    ThisCharacter.PerceptionService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Perception").Notes =
                    ThisCharacter.PerceptionService.Notes;
                //Sync Empathy
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Value =
                    ThisCharacter.EmpathyService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Points =
                    ThisCharacter.EmpathyService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Empathy").Notes =
                    ThisCharacter.EmpathyService.Notes;
                //Sync Willpower
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Value =
                    ThisCharacter.WillpowerService.Value;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Points =
                    ThisCharacter.WillpowerService.Points;
                ThisCharacter.PlayerAttributes.FirstOrDefault(a => a.BaseAttribute.Name == "Willpower").Notes =
                    ThisCharacter.WillpowerService.Notes;

                ThisCharacter = await PlayerCharacterService.CreatePlayerCharacter(ThisCharacter);
                return ThisCharacter;
            }
        }

        public async Task HandleOnValidAbilitySubmit()
        {
            Console.WriteLine($"{Helper.FormString}");
            var tempAbility = new PlayerAbility();
            tempAbility.BaseAbility = BaseAbilities.FirstOrDefault(a => a.Id == Int32.Parse(Helper.FormString));
            tempAbility = await PlayerAbilityService.CreatePlayerAbility(tempAbility);
            
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
        }

        protected BSModal Confirmation { get; set; }
        protected void onConfirmationToggle(MouseEventArgs e)
        {
            Confirmation.Toggle();
        }

        [CascadingParameter] public IModalService Modal { get; set; }
        protected void EditPlayerAbilityModal(int playerAbilityId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditPlayerAbility.EditPlayerAbilityId), playerAbilityId);

            Modal.Show<EditPlayerAbility>("Edit Ability", parameters);
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
