using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IPlayerAbilityRepository
    {
        Task<IEnumerable<PlayerAbility>> SearchPlayerAbilities(string search);
        Task<IEnumerable<PlayerAbility>> GetPlayerAbilities();
        Task<PlayerAbility> GetPlayerAbility(int playerAbilityId);
        Task<PlayerAbility> AddPlayerAbility(PlayerAbility playerAbility);
        Task<PlayerAbility> UpdatePlayerAbility(PlayerAbility playerAbility);
        Task<PlayerAbility> DeletePlayerAbility(int playerAbilityId);
    }
}
