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
        private readonly IUnitOfWork _unitOfWork;

        public RatingsController(
            ILogger<RatingsController> logger,            
            IUnitOfWork unitOfWork)
        {
            _logger = logger;            
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetRatings")]
        public async Task<IEnumerable<RatingDTO>> Get()
        {
            _logger.LogInformation("Getting ratings");

            var data = await _unitOfWork.RatingRepository.GetAsync();
            return data.Select(x => new RatingDTO
                {
                    Id = x.Id,
                    Rating = x.Type
                }).ToList();
        }
    }
}
