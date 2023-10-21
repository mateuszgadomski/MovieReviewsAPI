using Microsoft.EntityFrameworkCore;

namespace MovieReviewsAPI.Entities
{
    public class MovieReviewsDbContext : DbContext
    {
        public MovieReviewsDbContext(DbContextOptions<MovieReviewsDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(m =>
            {
                m.Property(m => m.Title)
                .HasMaxLength(25)
                .IsRequired();

                m.Property(m => m.Author)
                .HasMaxLength(55)
                .IsRequired();
            });

            modelBuilder.Entity<Category>(c =>
            {
                c.Property(c => c.Name)
                .HasMaxLength(20)
                .IsRequired();
            });

            modelBuilder.Entity<Review>(r =>
            {
                r.Property(r => r.Content)
                .HasMaxLength(20)
                .HasMaxLength(1000)
                .IsRequired();

                r.Property(r => r.IsWorth)
                 .IsRequired();
            });
        }
    }
}