using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Movie> MovieRepository { get; }
        public IRepository<Genre> GenreRepository { get; }
        public IRepository<Rating> RatingRepository { get; }
        public Task SaveChangesAsync();
    }
}
