
using Microsoft.EntityFrameworkCore;
using web_based_Bug_Management_Dashboard.Models.Domain;

namespace web_based_Bug_Management_Dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<BlogPost> BlogPosts { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.Property(x => x.Title)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Description)
                    .HasMaxLength(2000)
                    .IsRequired();

                entity.Property(x => x.ReporterName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.AssignedTo)
                    .HasMaxLength(100);

                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasMaxLength(32)
                    .IsRequired();
            });
        }
    }
}
