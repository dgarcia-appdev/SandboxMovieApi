namespace SandboxMovieApi.Infrastructure.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public short GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
