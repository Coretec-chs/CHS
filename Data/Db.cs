using Core.Model;
using System.Data.Entity;

namespace Data
{
    public class Db : DbContext
    {
        public Db()
        {
            Database.SetInitializer<Db>(null);
        }

        public virtual DbSet<AspNetMenus> AspNetMenus { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.Controller)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.RouteData)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetMenus>()
                .Property(e => e.MenuIcon)
                .IsUnicode(false);            

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);
        }
    }
}
