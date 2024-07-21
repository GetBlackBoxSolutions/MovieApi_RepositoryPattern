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
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Rating> _ratingRepository;

        public MoviesController(
            ILogger<MoviesController> logger,
            IRepository<Movie> movieRepository,
            IRepository<Genre> genreRepoitory,
            IRepository<Rating> ratingRepository
            )
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _genreRepository = genreRepoitory;
            _ratingRepository = ratingRepository;
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
                Description = m.Description,
                YearReleased = m.YearReleased,
                RatingId = m.RatingId,
                Rating = new RatingDTO
                {
                    Id = m.RatingId,
                    Rating = m.Rating.Type
                },
                Genres = m.MovieGenres.Select(mg => new GenreDTO { Id = mg.Id, Genre = mg.Type }).ToList(),
            }).ToList();                    
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MoviePostDTO movieDTO)
        {
            _logger.LogInformation("Adding a movie");
            
            var genres = await _genreRepository.GetAsync(g => movieDTO.Genres.Select(g => g.Id).Contains(g.Id));
            var rating = await _ratingRepository.GetAsync(r => r.Id == movieDTO.RatingId);

            var movie = new Movie
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                YearReleased = movieDTO.YearReleased,
                RatingId = rating.First().Id,
                MovieGenres = genres.ToList()
            };

            await _movieRepository.AddAsync(movie);
            return Ok();
        }   
    }
}
