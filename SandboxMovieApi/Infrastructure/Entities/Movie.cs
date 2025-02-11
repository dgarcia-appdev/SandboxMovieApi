namespace SandboxMovieApi.Infrastructure.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte RatingId { get; set; }
        public Rating Rating { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
    }

}