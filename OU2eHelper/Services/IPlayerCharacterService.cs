using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelper.Services
{
    public interface IPlayerCharacterService
    {
        Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters();
        Task<PlayerCharacter> UpdatePlayerCharacter(PlayerCharacter playerCharacter);
        Task<PlayerCharacter> CreatePlayerCharacter(PlayerCharacter playerCharacter);

    }
}
