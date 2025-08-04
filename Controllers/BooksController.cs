using BookDataGenerator.Models;
using BookDataGenerator.Services;
using Microsoft.AspNetCore.Mvc;
namespace BookDataGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookGeneratorService _bookGenerator;
        private readonly SeedService _seedService;
        public BooksController(
            BookGeneratorService bookGenerator,
            SeedService seedService)
        {
            _bookGenerator = bookGenerator;
            _seedService = seedService;
        }
        [HttpGet]
        public IActionResult GetBooks([FromQuery] GeneratorRequest request)
        {
            // Force Japanese content dynamically
            request.Region = "ja-JP"; // <-- Override region

            var books = _bookGenerator.GenerateBooks(request);
            return Ok(books);
        }
        public IActionResult GetBooks([FromQuery] GeneratorRequest request)
        {
            try
            {
                var books = _bookGenerator.GenerateBooks(request);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult GetBooks([FromQuery] GeneratorRequest request)
        {
            try
            {
                ValidateRequest(request);
                var books = _bookGenerator.GenerateBooks(request);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("random-seed")]
        public IActionResult GetRandomSeed()
        {
            return Ok(_seedService.GenerateRandomSeed());
        }
        private void ValidateRequest(GeneratorRequest request)
        {
            if (request.AvgLikes < 0 || request.AvgLikes > 10)
                throw new ArgumentException("Likes must be between 0 and 10");
            if (request.AvgReviews < 0)
                throw new ArgumentException("Reviews cannot be negative");
        }
        [HttpGet("italian")]
        public IActionResult GetItalianBooks([FromQuery] int seed = 42)
        {
            var request = new GeneratorRequest
            {
                Region = "it-IT",  // Hardcoded Italian region
                Seed = seed,
                AvgLikes = 0,      // Default values
                AvgReviews = 0,
                Page = 1
            };

            try
            {
                var italianBooks = _bookGenerator.GenerateBooks(request);
                return Ok(italianBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}