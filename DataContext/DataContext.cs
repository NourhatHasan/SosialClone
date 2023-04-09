using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sosialClone;

namespace Context
{
    public class DataContext : IdentityDbContext<user>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
       
       
        /*
         * we dont need to set a User DbSet since 
         * we take care of it by having a Identity DBContext 
        public DbSet<user> users { get; set; }
        */
        public DbSet<Entities> entities { get; set; }
    }
}
