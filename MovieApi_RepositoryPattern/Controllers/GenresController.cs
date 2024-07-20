using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IRepository<Genre> _genreRepository;
        public GenresController(
            ILogger<GenresController> logger,
            IRepository<Genre> genreRepository)
        {
            _logger = logger;
            _genreRepository = genreRepository;
        }
        [HttpGet(Name = "GetGenres")]
        public async Task<ICollection<GenreDTO>> Get()
        {
            _logger.LogInformation("Getting genres");
            var data = await _genreRepository.GetAsync();
            return data.Select(g => new GenreDTO
            {
                Id = g.Id,
                Genre = g.Type,
            }).ToList();
        }
    }
}
