using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Models;
using System.Data.Entity;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Fuente> Fuentes { get; set; }
        public DbSet<Letra> Letras { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRol> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Letra>().Property(m => m.Name).IsOptional();
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
