using backend.Core;
using backend.Enum;
using backend.Interface;
using backend.Models;
using static backend.Enum.Constants;

namespace backend.Services
{
    public class ExcelMergerService : BaseService<ExcelMergerService>, IExcelMergerService
    {
        private readonly IExcelMergerRepository _excelMergerRepository;

        public ExcelMergerService(IBaseDependenciesService<ExcelMergerService> dependencies, IExcelMergerRepository excelMergerRepository) : base(dependencies)
        {
            _excelMergerRepository = excelMergerRepository;
        }
        public async Task<List<UserResponse>> GetUserAsync(USER_STATUS_FILTER status = USER_STATUS_FILTER.All)
        {
            try
            {
                List<UserResponse> newUserResponse = new List<UserResponse>();
                var userList = await _excelMergerRepository.RetrieveUserAsync();
                if(userList != null && userList.Count > 0) 
                {
                    switch (status)
                    {
                        case USER_STATUS_FILTER.All:
                            newUserResponse = (List<UserResponse>)base.Mapper.Map<IList<User>, IList<UserResponse>>(userList);
                        break;
                        case USER_STATUS_FILTER.Active:
                            newUserResponse = (List<UserResponse>)base.Mapper.Map<IList<User>, IList<UserResponse>>(userList.Where(x => x.Status == true).ToList());
                        break;
                        case USER_STATUS_FILTER.Inactive:
                            newUserResponse = (List<UserResponse>)base.Mapper.Map<IList<User>, IList<UserResponse>>(userList.Where(x => x.Status == false).ToList());
                        break;
                        default:
                            newUserResponse = (List<UserResponse>)base.Mapper.Map<IList<User>, IList<UserResponse>>(userList.Where(x => x.Status == true).ToList());
                        break;
                    }
                }
                return newUserResponse;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
