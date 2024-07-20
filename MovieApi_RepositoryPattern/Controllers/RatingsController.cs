using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi_RepositoryPattern.DTOs;

namespace MovieApi_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly DataContext _dataContext;

        public RatingsController(
            ILogger<RatingsController> logger,
            DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet(Name = "GetRatings")]
        public async Task<IEnumerable<RatingDTO>> Get()
        {
            _logger.LogInformation("Getting ratings");
            return await _dataContext.Ratings.Select(r => new RatingDTO
            {
                Id = r.Id,
                Rating = r.Type
            }).ToListAsync();
        }
    }
}
