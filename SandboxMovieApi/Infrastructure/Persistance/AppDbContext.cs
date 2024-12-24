using Microsoft.EntityFrameworkCore;
using SandboxMovieApi.Entities;

namespace SandboxMovieApi.Infrastructure.Persistance
{
    /// <summary>
    /// DbContext class for the application
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Rating> Rating { get; set; }


        /// <summary>
        /// Configuring the DbContext
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieDb;Integrated Security=True;TrustServerCertificate=true");
        }
    }
}
