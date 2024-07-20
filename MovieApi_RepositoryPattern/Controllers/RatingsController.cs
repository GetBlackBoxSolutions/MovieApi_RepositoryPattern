using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IRepository<Rating> _ratingRepository;

        public RatingsController(
            ILogger<RatingsController> logger,            
            IRepository<Rating> ratingRepository)
        {
            _logger = logger;            
            _ratingRepository = ratingRepository;
        }

        [HttpGet(Name = "GetRatings")]
        public async Task<IEnumerable<RatingDTO>> Get()
        {
            _logger.LogInformation("Getting ratings");

            var data = await _ratingRepository.GetAsync();
            return data.Select(x => new RatingDTO
                {
                    Id = x.Id,
                    Rating = x.Type
                }).ToList();
        }
    }
}
