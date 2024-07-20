using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly DataContext _dataContext;

        public MoviesController(
            ILogger<MoviesController> logger,
            DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<ICollection<MovieDTO>> Get()
        {
            _logger.LogInformation("Getting movies");
            return await _dataContext.Movies
                    .Include(r => r.Rating)
                    .Include(g => g.MovieGenres)
                    .Select(m => new MovieDTO
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
                    })
                    .ToListAsync();
        }
    }
}
