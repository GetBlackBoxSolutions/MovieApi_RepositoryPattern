namespace MovieApi_RepositoryPattern.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int YearReleased { get; set; }
        
        public int RatingId { get; set; }
        public RatingDTO Rating { get; set; } = default!;
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    }
}
