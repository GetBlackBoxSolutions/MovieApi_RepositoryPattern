namespace ApplicationCore.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int YearReleased { get; set; }
        public int RatingId { get; set; }

        public Rating Rating { get; set; } = default!;
        public List<Genre> MovieGenres { get; set; } = new List<Genre>();
    }
}
