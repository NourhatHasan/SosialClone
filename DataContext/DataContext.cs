using Microsoft.EntityFrameworkCore;
using sosialClone;

namespace Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
       
      
        public DbSet<Entities> entities { get; set; }
    }
}
