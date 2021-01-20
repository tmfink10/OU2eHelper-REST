using System.Collections.Generic;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IBaseAttributeRepository
    {
        Task<IEnumerable<BaseAttribute>> SearchBaseAttributes(string search);
        Task<IEnumerable<BaseAttribute>> GetBaseAttributes();
        Task<BaseAttribute> GetBaseAttribute(int baseAttributeId);
        Task<BaseAttribute> AddBaseAttribute(BaseAttribute baseAttribute);
        Task<BaseAttribute> UpdateBaseAttribute(BaseAttribute baseAttribute);
        Task<BaseAttribute> DeleteBaseAttribute(int baseAttributeId);
    }
}
