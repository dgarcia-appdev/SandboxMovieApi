using System.ComponentModel.DataAnnotations.Schema;

namespace SandboxMovieApi.Infrastructure.Entities
{
    /// <summary>
    /// Rating Entity
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Id - The unique identifier for the rating.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        /// <summary>
        /// Description - The description of the rating.
        /// </summary>
        public string Description { get; set; } = default!;
    }
}
