using backend.Models;
using static backend.Enum.Constants;

namespace backend.Interface
{
    public interface IExcelMergerService
    {
        Task<List<UserResponse>> GetUserAsync(USER_STATUS_FILTER status);
    }
}
