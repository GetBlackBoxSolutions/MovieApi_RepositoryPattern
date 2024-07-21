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
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(
            ILogger<MoviesController> logger,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<ICollection<MovieDTO>> Get()
        {
            _logger.LogInformation("Getting movies");
            var data = await _unitOfWork.MovieRepository.GetAsync(includeProperties: "Rating,MovieGenres");
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
            
            var genres = await _unitOfWork.GenreRepository.GetAsync(g => movieDTO.Genres.Select(g => g.Id).Contains(g.Id));
            var rating = await _unitOfWork.RatingRepository.GetAsync(r => r.Id == movieDTO.RatingId);

            var movie = new Movie
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                YearReleased = movieDTO.YearReleased,
                RatingId = rating.First().Id,
                MovieGenres = genres.ToList()
            };

            await _unitOfWork.MovieRepository.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting a movie");
            await _unitOfWork.MovieRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
