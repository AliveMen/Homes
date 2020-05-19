using Homes.Models;
using Microsoft.EntityFrameworkCore;

namespace Homes.Repositories
{
    public class HomesDbContext : DbContext
    {
        protected HomesDbContext()
        {
        }

        public HomesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().ToTable("Building").HasKey(x => x.Id);
            modelBuilder.Entity<Building>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();


            modelBuilder.Entity<Tenant>().ToTable("Tenant").HasKey(x => x.Id);
            modelBuilder.Entity<Tenant>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tenant>().HasOne(m => m.Building).WithMany(x => x.Tenants).HasForeignKey(x => x.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(
            "Data Source = (local); Initial Catalog = Homes; Persist Security Info=True;User ID = virto; Password=virto;MultipleActiveResultSets=True;Connect Timeout = 30");
    }
}