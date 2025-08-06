using Microsoft.AspNetCore.Mvc;
using BookDataGenerator.Services;
using BookDataGenerator.Models;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookGeneratorService _bookGenerator;

    public BookController(BookGeneratorService bookGenerator)
    {
        _bookGenerator = bookGenerator;
    }

    [HttpGet("batch")]
    public IActionResult GetBatch(
        string language = "en-US",
        int page = 1,
        int pageSize = 20,
        double avgLikes = 5.0,
        double avgReviews = 5.0,
        string seed = "default")
    {
        var books = _bookGenerator.GenerateBooks(language, page, pageSize, avgLikes, avgReviews, seed);
        return Ok(books);
    }
}