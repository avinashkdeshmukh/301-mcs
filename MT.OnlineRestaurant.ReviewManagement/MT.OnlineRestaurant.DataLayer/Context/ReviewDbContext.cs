using Microsoft.EntityFrameworkCore;
using System;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=Reivew;Uid=avinashd; Pwd=Avinash@2112;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:demolab.database.windows.net,1433;Initial Catalog=ReviewDB;Persist Security Info=False;User ID=admin123;Password=Passwrd@123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(r => r.UpdatedBy)
                .IsRequired(false);
                entity.Property(r => r.UpdatedDateTime)
                .IsRequired(false);
            });
        }
    }
}
