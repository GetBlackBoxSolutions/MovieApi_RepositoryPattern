using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IRepository<Movie> _movieRepository;

        public MoviesController(
            ILogger<MoviesController> logger,
            IRepository<Movie> movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<ICollection<MovieDTO>> Get()
        {
            _logger.LogInformation("Getting movies");
            var data = await _movieRepository.GetAsync(includeProperties: "Rating,MovieGenres");
            return data.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                YearReleased = m.YearReleased,
                Rating = new RatingDTO
                {
                    Id = m.RatingId,
                    Rating = m.Rating.Type
                },
                Genres = m.MovieGenres.Select(mg => new GenreDTO { Id = mg.Id, Genre = mg.Type }).ToList(),
            }).ToList();                    
        }
    }
}
