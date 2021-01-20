using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelper.Services
{
    public interface IBaseSkillService
    {
        Task<IEnumerable<BaseSkill>> GetBaseSkills();
    }
}
