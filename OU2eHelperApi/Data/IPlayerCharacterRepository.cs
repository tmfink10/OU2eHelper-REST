using System.Collections.Generic;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IPlayerCharacterRepository
    {
        Task<IEnumerable<PlayerCharacter>> SearchPlayerCharacters(string search);
        Task<IEnumerable<PlayerCharacter>> GetPlayerCharacters();
        Task<PlayerCharacter> GetPlayerCharacter(int playerCharacterId);
        Task<PlayerCharacter> AddPlayerCharacter(PlayerCharacter playerCharacter);
        Task<PlayerCharacter> UpdatePlayerCharacter(PlayerCharacter playerCharacter);
        Task<PlayerCharacter> DeletePlayerCharacter(int playerCharacterId);
    }
}
