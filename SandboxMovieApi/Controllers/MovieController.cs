using Microsoft.AspNetCore.Mvc;
using SandboxMovieApi.Infrastructure.Entities;
using SandboxMovieApi.Infrastructure;
using SandboxMovieApi.Models;

namespace SandboxMovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IRepository<Movie> _movieRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieRepo"></param>
        public MovieController(IRepository<Movie> movieRepo)
        {
            _movieRepo = movieRepo;
        }

        /// <summary>
        /// Retrieves all Ratings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Movies")]
        public ActionResult<IEnumerable<Movie>> GetMovies() 
        {
            var movies = _movieRepo.Get(includeProperties: "Rating,MovieGenres.Genre");
            return Ok(movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                RatingId = m.RatingId,
                Rating = m.Rating.Description,
                Genres = m.MovieGenres
                    .Select(mg => new GenreDTO { Id = mg.GenreId, Description = mg.Genre.Description })
                    .ToList()
            }));
        }
    }
}
