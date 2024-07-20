namespace ApplicationCore.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
