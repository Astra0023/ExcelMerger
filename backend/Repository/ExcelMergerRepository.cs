using backend.Context;
using backend.Enum;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using static backend.Enum.Constants;

namespace backend.Repository
{
    public class ExcelMergerRepository : IExcelMergerRepository
    {
        private readonly ExcelMergerDbContext _dbContext;
        
        public ExcelMergerRepository(ExcelMergerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> RetrieveUserAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
