using Microsoft.AspNetCore.Mvc;
using SandboxMovieApi.Entities;

namespace SandboxMovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        [HttpGet("Ratings")]
        public ActionResult<IEnumerable<Rating>> GetRating()
        {
            var rating = new Rating();
            rating.Id = 1;
            rating.Name = "Test";

            var ratingList = new List<Rating>();
            ratingList.Add(rating);

            return Ok(ratingList);
        }

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
            var rating = new Rating();
            rating.Id = 1;
            rating.Name = "Test";


            if (ratingId != rating.Id)
            {
                return NotFound("Could not find movie");
            }

            return Ok(rating);
        }
    }
}
