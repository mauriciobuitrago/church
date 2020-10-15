using Church.Common.Entities;
using Church.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<Campus> campuses { get; set; }
        public DbSet<Churchi> churches { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Profession> Professions { get; set; }

        public DbSet<Assistance> Assistances  { get; set; }
        public DbSet<Meeting> Meetings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campus>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Churchi>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<District>()
             .HasIndex(t => t.Name)
             .IsUnique();

            modelBuilder.Entity<Profession>()
           .HasIndex(t => t.Name)
           .IsUnique();

        }
    }

}
