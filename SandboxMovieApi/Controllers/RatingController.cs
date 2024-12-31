using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SandboxMovieApi.Entities;
using SandboxMovieApi.Infrastructure;
using SandboxMovieApi.Infrastructure.Persistance;

namespace SandboxMovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> _ratingRepo;

        public RatingController(IRepository<Rating> ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        [HttpGet("Ratings")]
        public ActionResult<IEnumerable<Rating>> GetRating()
        {
            var ratingsInfo = _ratingRepo.Get();

            return Ok(ratingsInfo);
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
            rating.Description = "Test";


            if (ratingId != rating.Id)
            {
                return NotFound("Could not find movie");
            }

            return Ok(rating);
        }
    }
}
