using BookDataGenerator.Models;
using BookDataGenerator.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookDataGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly BookGeneratorService _bookGenerator;
        private readonly DataExportService _exportService;

        public ExportController(BookGeneratorService bookGenerator, DataExportService exportService)
        {
            _bookGenerator = bookGenerator;
            _exportService = exportService;
        }

        [HttpGet("csv")]
        public IActionResult ExportCsv([FromQuery] GeneratorRequest request, [FromQuery] int pages = 1)
        {
            try
            {
                var allBooks = new List<Book>();
                
                for (int page = 1; page <= pages; page++)
                {
                    var books = _bookGenerator.GenerateBooks(
                        request.Region ?? "en-US",
                        page,
                        20,
                        request.AvgLikes ?? 5.0,
                        request.AvgReviews ?? 5.0,
                        request.Seed?.ToString() ?? "default");
                    allBooks.AddRange(books);
                }

                var csvBytes = _exportService.ExportToCsv(allBooks);
                var fileName = $"books-{request.Region}-seed{request.Seed}-pages{pages}.csv";
                
                return File(csvBytes, "text/csv", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
