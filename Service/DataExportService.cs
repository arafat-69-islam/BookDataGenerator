using BookDataGenerator.Models;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace BookDataGenerator.Services
{
    public class DataExportService
    {
        public byte[] ExportToCsv(List<Book> books)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(books);
            writer.Flush();
            return memoryStream.ToArray();
        }
    }
}
