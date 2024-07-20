using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly DataContext _dataContext;
        public GenresController(
            ILogger<GenresController> logger,
            DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }
        [HttpGet(Name = "GetGenres")]
        public async Task<ICollection<GenreDTO>> Get()
        {
            _logger.LogInformation("Getting genres");
            return await _dataContext.Genres.Select(g => new GenreDTO
            {
                Id = g.Id,
                Genre = g.Type,
            }).ToListAsync();
        }
    }
}
