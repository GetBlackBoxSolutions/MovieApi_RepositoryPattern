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
        private readonly IUnitOfWork _unitOfWork;
        public GenresController(
            ILogger<GenresController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet(Name = "GetGenres")]
        public async Task<ICollection<GenreDTO>> Get()
        {
            _logger.LogInformation("Getting genres");
            var data = await _unitOfWork.GenreRepository.GetAsync();
            return data.Select(g => new GenreDTO
            {
                Id = g.Id,
                Genre = g.Type,
            }).ToList();
        }
    }
}
