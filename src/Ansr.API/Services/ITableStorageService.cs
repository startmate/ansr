using ansr.API.Models;
using System.Threading.Tasks;

namespace ansr.API.Services
{
    public interface ITableStorageService
    {
        Task<QuestionEntity> RetrieveAsync(string category, string id);
        Task<QuestionEntity> InsertOrMergeAsync(QuestionEntity entity);
        Task<QuestionEntity> DeleteAsync(QuestionEntity entity);
    }
}
