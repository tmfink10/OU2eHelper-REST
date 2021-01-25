using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelper.Services
{
    public interface IBaseAbilityService
    {
        Task<IEnumerable<BaseAbility>> GetBaseAbilities();
        Task<BaseAbility> GetBaseAbility(int id);
    }
}
