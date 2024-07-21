using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IRepository<Rating> _ratingRepository;
        private IRepository<Genre> _genreRepository;
        private IRepository<Movie> _movieRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            _movieRepository = new Repository<Movie>(context);
            _ratingRepository = new Repository<Rating>(context);
            _genreRepository = new Repository<Genre>(context);
        }

        public IRepository<Movie> MovieRepository 
        { 
            get 
            { 
                if(_movieRepository == null)
                {
                    _movieRepository = new Repository<Movie>(_context);
                }
                return _movieRepository;
            } 
        }

        public IRepository<Genre> GenreRepository 
        {
            get
            {
                if (_genreRepository == null)
                {
                    _genreRepository = new Repository<Genre>(_context);
                }
                return _genreRepository;
            }
        } 
        
        public IRepository<Rating> RatingRepository
        {
            get
            {
                if(_ratingRepository == null)
                {
                    _ratingRepository = new Repository<Rating>(_context);
                }
                return _ratingRepository;
            }
        }

        public async Task SaveChangesAsync() 
        {
            await _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
