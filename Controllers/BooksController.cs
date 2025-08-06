using BookDataGenerator.Models;
using BookDataGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookDataGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookGeneratorService _bookGenerator;
        private readonly SeedService _seedService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookGeneratorService bookGenerator, SeedService seedService, ILogger<BooksController> logger)
        {
            _bookGenerator = bookGenerator;
            _seedService = seedService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] GeneratorRequest request)
        {
            try
            {
                ValidateRequest(request);
                var books = _bookGenerator.GenerateBooks(request);
                return Ok(ApiResponse<List<Book>>.Ok(books));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating books with request {@Request}", request);
                return BadRequest(ApiResponse<List<Book>>.Fail(ex.Message));
            }
        }

        [HttpGet("random-seed")]
        public IActionResult GetRandomSeed()
        {
            var seed = _seedService.GenerateRandomSeed();
            return Ok(ApiResponse<int>.Ok(seed));
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is reachable");
        }

        private void ValidateRequest(GeneratorRequest request)
        {
            if (!request.AvgLikes.HasValue || request.AvgLikes < 0 || request.AvgLikes > 10)
            {
                _logger.LogWarning("Invalid AvgLikes value: {AvgLikes}", request.AvgLikes);
                throw new ArgumentException("Likes must be between 0 and 10");
            }

            if (!request.AvgReviews.HasValue || request.AvgReviews < 0)
            {
                _logger.LogWarning("Invalid AvgReviews value: {AvgReviews}", request.AvgReviews);
                throw new ArgumentException("Reviews cannot be negative");
            }

            if (!request.Page.HasValue || request.Page < 1)
            {
                _logger.LogWarning("Invalid Page value: {Page}", request.Page);
                throw new ArgumentException("Page must be greater than 0");
            }
        }
    }
}
