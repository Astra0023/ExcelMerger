using backend.Models;
using static backend.Enum.Constants;

namespace backend.Interface
{
    public interface IExcelMergerRepository
    {
        Task<List<User>> RetrieveUserAsync();
    }
}
