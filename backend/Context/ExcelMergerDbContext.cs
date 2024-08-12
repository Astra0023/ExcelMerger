using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public partial class ExcelMergerDbContext : DbContext
    {
        public ExcelMergerDbContext()
        {

        }

        public ExcelMergerDbContext(DbContextOptions<ExcelMergerDbContext> options) : base(options) { }

        #region UserLookup
        public virtual DbSet<User> Users { get; set; }
        #endregion
    }
}
