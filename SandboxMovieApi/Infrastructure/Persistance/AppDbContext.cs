using Microsoft.EntityFrameworkCore;
using SandboxMovieApi.Infrastructure.Entities;

namespace SandboxMovieApi.Infrastructure.Persistance
{
    /// <summary>
    /// DbContext class for the application
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }

        public DbSet<MovieGenre> MovieGenre { get; set; }


        public AppDbContext(DbContextOptions options) 
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne<Movie>(sc => sc.Movie)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(sc => sc.MovieId);


            modelBuilder.Entity<MovieGenre>()
                .HasOne<Genre>(sc => sc.Genre)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(sc => sc.GenreId);
        }
    }
}
