using Microsoft.AspNetCore.Mvc;
using SandboxMovieApi.Infrastructure;
using SandboxMovieApi.Infrastructure.Entities;
using SandboxMovieApi.Models;

namespace SandboxMovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository<Genre> _genreRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genreRepo"></param>
        public GenresController(IRepository<Genre> genreRepo)
        {
            _genreRepo = genreRepo;
        }

        /// <summary>
        /// Retrieves all Genre.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Genre")]
        public ActionResult<IEnumerable<Genre>> GetRating() => Ok(_genreRepo.Get());

        /// <summary>
        /// Retrieves a Genre.
        /// </summary> 
        /// <response code="200">Returns the genre object</response>
        /// <response code="404">If the item can not be found</response>
        [HttpGet("Genre/{genreId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Genre> GetGenreById(int genreId)
        {
            var genre = _genreRepo.Get(genreId);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        /// <summary>
        /// Adds a new Genre.
        /// </summary>
        /// <param name="rating"></param>
        /// <response code="201">Returns the new genre</response>        
        /// <response>code="400">If the genre already exist</response>
        [HttpPost("Genre")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Genre> PostGenre(UpsertGenreDTO genre)
        {
            var genreDb = _genreRepo.Get(r => r.Description == genre.Description);
            if (genreDb.Count() > 0)
            {
                return BadRequest("Genre already exist");
            }

            var newGenre = new Genre
            {
                Description = genre.Description
            };

            _genreRepo.Add(newGenre);
            return CreatedAtAction("Genre", new { id = newGenre.Id }, genre);
        }

        /// <summary>
        /// Update a Genre
        /// </summary>
        /// <returns></returns>
        [HttpPut("Genre/{id}")]
        public ActionResult<Genre> PutGenre(int id, UpsertGenreDTO genre)
        {
            try
            {
                var genreDb = _genreRepo.Get(id);
                if (genreDb == null)
                {
                    return NotFound();
                }

                genreDb.Description = genre.Description;
                var numberOfUpdates = _genreRepo.Update(genreDb);

                if (numberOfUpdates == 0)
                {
                    return BadRequest("Error Occured: No Records updated");
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Error Occured: Contact Support 1-800-idontcare if issue persist");
            }
        }

        /// <summary>
        /// Delete a Genre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Genre> DeleteRating(int id)
        {
            try
            {
                var genreDb = _genreRepo.Get((byte)id);
                if (genreDb == null)
                {
                    return NotFound();
                }

                var numberOfUpdates = _genreRepo.Delete(genreDb);

                if (numberOfUpdates == 0)
                {
                    return BadRequest("Error Occured: No Records updated");
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Error Occured: Contact Support 1-800-idontcare if issue persist");
            }
        }
    }
}
