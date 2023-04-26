using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sosialClone;

namespace Context
{
    public class DataContext : IdentityDbContext<AppUser>
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
        public DbSet<Photo> photos { get; set; }


        //to get a primary key which is combunation of userId and entityId
        // and Configure the entity (one user will have many activities
        // and one activity will have many users)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //form the primary key
            builder.Entity<EntityUser>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

            builder.Entity<EntityUser>()
                   .HasOne(u => u.AppUser)
                   .WithMany(a => a.activities)
                   .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<EntityUser>()
                .HasOne(a => a.Activity)
                .WithMany(u => u.Attendies)
                .HasForeignKey(aa => aa.ActivityId);
        }
    }
}
