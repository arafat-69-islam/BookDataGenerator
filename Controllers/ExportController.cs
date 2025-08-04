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
        public ExportController(
            BookGeneratorService bookGenerator,
            DataExportService exportService)
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
                    request.Page = page;
                    allBooks.AddRange(_bookGenerator.GenerateBooks(request));
                }
                var csvBytes = _exportService.ExportToCsv(allBooks);
                return File(csvBytes, "text/csv", $"books-{request.Region}-seed{request.Seed}.csv");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}