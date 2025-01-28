namespace SandboxMovieApi.Models
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte RatingId { get; set; }
        public string Rating { get; set; }
        public List<GenreDTO> Genres { get; set; }
    }
}
