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
        public DbSet<EntityUser> EntityUser { get; set; }


        //to get a primary key which is combunation of userId and entityId
        // and Configure the entity (one user will have many activities
        // and one activity will have many users)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //form the primary key
            builder.Entity<EntityUser>(x => x.HasKey(aa => new { aa.userId, aa.EntityId }));

            builder.Entity<EntityUser>()
                   .HasOne(u => u.user)
                   .WithMany(a => a.entities)
                   .HasForeignKey(aa => aa.userId);

            builder.Entity<EntityUser>()
                .HasOne(a => a.entities)
                .WithMany(u => u.users)
                .HasForeignKey(aa => aa.EntityId);
        }
    }
}
