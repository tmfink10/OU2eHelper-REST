using System.Collections.Generic;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IBaseAbilityRepository
    {
        Task<IEnumerable<BaseAbility>> SearchBaseAbilities(string search);
        Task<IEnumerable<BaseAbility>> GetBaseAbilities();
        Task<BaseAbility> GetBaseAbility(int baseAbilityId);
        Task<BaseAbility> AddBaseAbility(BaseAbility baseAbility);
        Task<BaseAbility> UpdateBaseAbility(BaseAbility baseAbility);
        Task<BaseAbility> DeleteBaseAbility(int baseAbilityId);
    }
}
