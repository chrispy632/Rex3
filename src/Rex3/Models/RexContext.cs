using Microsoft.EntityFrameworkCore;

namespace Rex3.Models
{

    public class RexContext : DbContext
    {
        public RexContext(DbContextOptions<RexContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });

            builder.Entity<UserRole>()
                .HasOne(m => m.User)
                .WithMany(ma => ma.UserRoles)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserRole>()
                .HasOne(m => m.Role)
                .WithMany(ma => ma.UserRoles)
                .HasForeignKey(a => a.RoleId);


            builder.Entity<UserRole>()
                .HasKey(c => new { c.UserId, c.RoleId });


        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
