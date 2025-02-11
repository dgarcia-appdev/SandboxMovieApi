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
        private readonly IRepository<Rating> _ratingRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieRepo"></param>
        /// <param name="ratingRepo"></param>
        public MovieController(
            IRepository<Movie> movieRepo,
            IRepository<Rating> ratingRepo
            )
        {
            _movieRepo = movieRepo;
            _ratingRepo = ratingRepo;
        }

        /// <summary>
        /// Retrieves all Ratings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Movies")]
        public ActionResult<IEnumerable<MovieDTO>> GetMovies() 
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

        /// <summary>
        /// Retrieves a single movie by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Movies/{id}")]
        public ActionResult<MovieDTO> GetMovie(int id) 
        {
            var movie = _movieRepo.Get(m => m.Id == id, "Rating,MovieGenres.Genre");
            var tempMovie = movie.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                RatingId = m.RatingId,
                Rating = m.Rating.Description,
                Genres = m.MovieGenres
                    .Select(mg => new GenreDTO { Id = mg.GenreId, Description = mg.Genre.Description })
                    .ToList()
            }).FirstOrDefault();

            if(tempMovie == null)
            {
                return NotFound();
            }

            return Ok(tempMovie);
        }

        /// <summary>
        /// Post a new movie.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost("Movies")]
        public ActionResult<MovieDTO> PostMovie(MovieDTO movie)
        {
            var ratingDb = _ratingRepo.Get(movie.RatingId);
            if (ratingDb == null) 
            {
                return NotFound("Rating was not found");
            }

            _movieRepo.Add(new Movie() {
                Title = movie.Title,
                Description = movie.Description,
                RatingId = movie.RatingId,
                MovieGenres = movie.Genres.Select(g => new MovieGenre { GenreId = (short)g.Id }).ToList()
            });
            return CreatedAtAction("GetMovies", new { id = movie.Id }, movie);
        }
    }
}
