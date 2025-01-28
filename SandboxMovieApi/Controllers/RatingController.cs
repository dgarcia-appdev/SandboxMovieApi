using Microsoft.AspNetCore.Mvc;
using SandboxMovieApi.Infrastructure;
using SandboxMovieApi.Infrastructure.Entities;
using SandboxMovieApi.Models;

namespace SandboxMovieApi.Controllers
{
    /// <summary>
    /// Rating Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]   
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> _ratingRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ratingRepo"></param>
        public RatingController(IRepository<Rating> ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        /// <summary>
        /// Retrieves all Ratings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Ratings")]
        public ActionResult<IEnumerable<Rating>> GetRating() => Ok(_ratingRepo.Get());

        /// <summary>
        /// Retrieves a Rating.
        /// </summary> 
        /// <response code="200">Returns the rating object</response>
        /// <response code="404">If the item can not be found</response>
        [HttpGet("Ratings/{ratingId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Rating> GetRatingById(int ratingId)
        {
            var rating = _ratingRepo.Get((byte)ratingId);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        /// <summary>
        /// Adds a new Rating.
        /// </summary>
        /// <param name="rating"></param>
        /// <response code="201">Returns the new Rating</response>        
        /// <response>code="400">If the Rating already exist</response>
        [HttpPost("Rating")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Rating> PostRating(UpsertRatingDTO rating)
        {
            var ratingsDb = _ratingRepo.Get(r => r.Description == rating.Description);            
            if (ratingsDb.Count() > 0) 
            {
                return BadRequest("Rating already exist");
            }

            var newRating = new Rating
            {
                Description = rating.Description
            };

            _ratingRepo.Add(newRating);
            return CreatedAtAction(nameof(GetRatingById), new { ratingId = newRating.Id }, newRating);
        }

        /// <summary>
        /// Update a Rating
        /// </summary>
        /// <returns></returns>
        [HttpPut("Rating/{id}")]
        public ActionResult<Rating> PutRating(int id, UpsertRatingDTO rating)
        {
            try
            {
                var ratingDb = _ratingRepo.Get((byte)id);
                if (ratingDb == null)
                {
                    return NotFound();
                }

                ratingDb.Description = rating.Description;
                var numberOfUpdates = _ratingRepo.Update(ratingDb);

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
        /// Delete a Rating
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Rating> DeleteRating(int id)
        {
            try 
            {
                var ratingDb = _ratingRepo.Get((byte)id);
                if (ratingDb == null)
                {
                    return NotFound();
                }

                var numberOfUpdates = _ratingRepo.Delete(ratingDb);

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
