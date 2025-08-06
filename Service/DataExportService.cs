using BookDataGenerator.Models;
using CsvHelper;
using System.Globalization;

namespace BookDataGenerator.Services
{
    public class DataExportService
    {
        public byte[] ExportToCsv(List<Book> books)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            // Write custom headers
            csv.WriteField("Index");
            csv.WriteField("ISBN");
            csv.WriteField("Title");
            csv.WriteField("Author");
            csv.WriteField("Publisher");
            csv.WriteField("Likes");
            csv.WriteField("Reviews");
            csv.NextRecord();

            // Write data
            foreach (var book in books)
            {
                csv.WriteField(book.Index);
                csv.WriteField(book.ISBN);
                csv.WriteField(book.Title);
                csv.WriteField(book.Author);
                csv.WriteField(book.Publisher);
                csv.WriteField(book.Likes);
                csv.WriteField(book.Reviews.Count);
                csv.NextRecord();
            }

            writer.Flush();
            return memoryStream.ToArray();
        }
    }
}