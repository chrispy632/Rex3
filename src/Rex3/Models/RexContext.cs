using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rex3.ViewModels;

namespace Rex3.Models
{

    public class RexContext : DbContext
    {
        public RexContext(DbContextOptions<RexContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserListVM> UserListVM { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Role>()
                .ToTable("Roles");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserRole>().ToTable("UserRole");
            builder.Entity<Country>().ToTable("Country");

            builder.Entity("Rex3.Models.UserRole", b =>
            {
                b.HasKey("Id");

                b.HasOne("Rex3.Models.User", "User")
                    .WithMany("UserRoles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Rex3.Models.Role", "Role")
                    .WithMany("UserRoles")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
