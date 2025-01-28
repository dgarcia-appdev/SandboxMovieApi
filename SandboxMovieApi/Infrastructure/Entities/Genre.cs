namespace SandboxMovieApi.Infrastructure.Entities
{
    public class Genre
    {
        public short Id { get; set; }
        public string Description { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
    }
}
