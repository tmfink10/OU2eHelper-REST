using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IPlayerSkillRepository
    {
        Task<IEnumerable<PlayerSkill>> SearchPlayerSkills(string search);
        Task<IEnumerable<PlayerSkill>> GetPlayerSkills();
        Task<PlayerSkill> GetPlayerSkill(int playerSkillId);
        Task<PlayerSkill> AddPlayerSkill(PlayerSkill playerSkill);
        Task<PlayerSkill> UpdatePlayerSkill(PlayerSkill playerSkill);
        Task<PlayerSkill> DeletePlayerSkill(int playerSkillId);
    }
}
