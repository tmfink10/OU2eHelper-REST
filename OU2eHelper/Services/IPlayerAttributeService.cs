using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelper.Services
{
    public interface IPlayerAttributeService
    {
        Task<IEnumerable<PlayerAttribute>> GetPlayerAttributes();
        Task<PlayerAttribute> GetPlayerAttribute(int Id);
        Task<PlayerAttribute> UpdatePlayerAttribute(int Id, PlayerAttribute playerAttribute);
        Task<PlayerAttribute> CreatePlayerAttribute(PlayerAttribute playerAttribute);
    }
}
