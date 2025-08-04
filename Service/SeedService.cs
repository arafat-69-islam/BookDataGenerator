
using BookDataGenerator.Models;
using CsvHelper; 
using System.Collections.Generic;
using System.Formats.Asn1;
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

            csv.WriteRecords(books);
            writer.Flush();
            return memoryStream.ToArray();
        }
    }
}