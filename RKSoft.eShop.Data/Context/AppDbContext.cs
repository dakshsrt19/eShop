using Microsoft.EntityFrameworkCore;
using RKSoft.eShop.Model.Entities;

namespace RKSoft.eShop.Data.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EStore>().ToTable("Stores");
        }
        public virtual DbSet<EStore> Stores { get; set; }
    }
}
