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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

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

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Review>(r =>
            {
                r.Property(r => r.Content)
                .HasMaxLength(1000)
                .IsRequired();

                r.Property(r => r.IsWorth)
                 .IsRequired();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.Property(u => u.Email)
                    .IsRequired();

                u.Property(u => u.Login)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();
        }
    }
}