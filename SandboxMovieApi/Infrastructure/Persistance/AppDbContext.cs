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

        public AppDbContext(DbContextOptions options) 
            : base(options)
        { 
        }
        
    }
}
