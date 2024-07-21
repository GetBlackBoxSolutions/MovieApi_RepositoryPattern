using System.ComponentModel.DataAnnotations;

namespace MovieApi_RepositoryPattern.DTOs
{
    public class MoviePostDTO
    {
        [Required]
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int YearReleased { get; set; }
        public int RatingId { get; set; }
        public ICollection<GenreDTO> Genres { get; set; } = default!;
    }
}
