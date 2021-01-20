using System.Collections.Generic;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IBaseSkillRepository
    {
        Task<IEnumerable<BaseSkill>> SearchBaseSkills(string search);
        Task<IEnumerable<BaseSkill>> GetBaseSkills();
        Task<BaseSkill> GetBaseSkill(int baseSkillId);
        Task<BaseSkill> AddBaseSkill(BaseSkill baseSkill);
        Task<BaseSkill> UpdateBaseSkill(BaseSkill baseSkill);
        Task<BaseSkill> DeleteBaseSkill(int baseSkillId);
    }
    
}
