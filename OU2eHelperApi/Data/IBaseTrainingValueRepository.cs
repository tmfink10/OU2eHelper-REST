using System.Collections.Generic;
using System.Threading.Tasks;
using OU2eHelperModels.Models;

namespace OU2eHelperApi.Data
{
    public interface IBaseTrainingValueRepository
    {
        Task<IEnumerable<BaseTrainingValue>> SearchBaseTrainingValues(string search);
        Task<IEnumerable<BaseTrainingValue>> GetBaseTrainingValues();
        Task<BaseTrainingValue> GetBaseTrainingValue(int baseTrainingValueId);
        Task<BaseTrainingValue> AddBaseTrainingValue(BaseTrainingValue baseTrainingValue);
        Task<BaseTrainingValue> UpdateBaseTrainingValue(BaseTrainingValue baseTrainingValue);
        Task<BaseTrainingValue> DeleteBaseTrainingValue(int baseTrainingValueId);
    }
}
