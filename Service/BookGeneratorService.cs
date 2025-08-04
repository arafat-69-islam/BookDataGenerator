using BookDataGenerator.Models;
using System;
using System.Collections.Generic;
namespace BookDataGenerator.Services
using BookDataGenerator.Models;
using System.Text.Json;
{
    public class BookGeneratorService
    {
    private readonly Dictionary<string, RegionData> _regionData;
    private readonly DataLoaderService _dataLoader;

    public BookGeneratorService(DataLoaderService dataLoader)
    {
        _regionData = new Dictionary<string, RegionData>
        {
            _dataLoader = dataLoader;
        _regionData = LoadAllRegionData();
            ["it-IT"] = LoadRegionData(dataLoader, "it-IT")
        };
    }

    private Dictionary<string, RegionData> LoadAllRegionData()
    {
        return new Dictionary<string, RegionData>
        {
            // Existing regions...
            ["it-IT"] = new RegionData
            {
                Titles = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("BookTitles.json")["it-IT"],
                FirstNames = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json")["firstNames"]["it-IT"],
                // ... other properties


                var reviewData = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("ReviewData.json");
            }
        };
        return new Dictionary<string, RegionData>
        {
            ["en-US"] = new RegionData
            {
                Titles = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("BookTitles.json")["en-US"],
                FirstNames = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json")["firstNames"]["en-US"],
                LastNames = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json")["lastNames"]["en-US"],
                Publishers = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("Publishers.json")["en-US"],
                ReviewTexts = reviewData["reviewTexts"],
                Companies = reviewData["companies"]
            },
            ["fr-FR"] = new RegionData
            {
                Titles = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("BookTitles.json")["fr-FR"],
                FirstNames = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json")["firstNames"]["fr-FR"],
                LastNames = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json")["lastNames"]["fr-FR"],
                Publishers = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("Publishers.json")["fr-FR"],
                ReviewTexts = reviewData["reviewTexts"],
                Companies = reviewData["companies"]
            },
            // Add other regions (de-DE, es-ES, ja-JP) following same pattern
        };
    }
    public List<Book> GenerateBooks(GeneratorRequest request, int count = 20)
    {
        var combinedSeed = request.Seed + request.Page;
        var random = new Random(combinedSeed);

        if (!_regionData.TryGetValue(request.Region, out var region))
            throw new ArgumentException($"Unsupported region: {request.Region}");

        var books = new List<Book>();
        for (int i = 0; i < count; i++)
        {
            var bookRandom = new Random(combinedSeed + i);
            books.Add(GenerateSingleBook(request, region, bookRandom, i));
        }
        return books;
    }
    private Book GenerateSingleBook(GeneratorRequest request, RegionData region, Random random, int index)
    {
        var book = new Book
        {
            Index = (request.Page - 1) * 20 + index + 1,
            ISBN = GenerateISBN(random),
            Title = region.Titles[random.Next(region.Titles.Length)],
            Author = region.GetRandomAuthor(random),
            Publisher = region.Publishers[random.Next(region.Publishers.Length)],
            Likes = CalculateFractionalCount(request.AvgLikes, random),
            Region = request.Region,
            CoverImageUrl = GenerateCoverUrl(request.Region, random)
        };

        book.Reviews = GenerateReviews(book, request.AvgReviews, random, region);
        return book;
    }
    private readonly Dictionary<string, FakerData> _regionData;
        public BookGeneratorService()
        {
            _regionData = new Dictionary<string, FakerData>
            {
                ["en-US"] = new FakerData("English", "USA"),
                ["fr-FR"] = new FakerData("French", "France"),
                ["de-DE"] = new FakerData("German", "Germany"),
                ["es-ES"] = new FakerData("Spanish", "Spain"),
                ["ja-JP"] = new FakerData("Japanese", "Japan")
            };
        }
        public List<Book> GenerateBooks(GeneratorRequest request, int count = 20)
        {
            var combinedSeed = request.Seed + request.Page;
            var random = new Random(combinedSeed);
            var region = _regionData[request.Region];
            var books = new List<Book>();
            for (int i = 0; i < count; i++)
            {
                var bookRandom = new Random(combinedSeed + i);
                books.Add(GenerateSingleBook(request, region, bookRandom, i));
            }
            return books;
        }
        private Book GenerateSingleBook(GeneratorRequest request, FakerData region, Random random, int index)
        {
            var book = new Book
            {
                Index = (request.Page - 1) * 20 + index + 1,
                ISBN = GenerateISBN(random),
                Title = region.Titles[random.Next(region.Titles.Length)],
                Author = region.GetRandomAuthor(random),
                Publisher = region.Publishers[random.Next(region.Publishers.Length)],
                Likes = CalculateFractionalCount(request.AvgLikes, random),
                Region = request.Region,
                CoverImageUrl = GenerateCoverUrl(request.Region, random)
            };
            book.Reviews = GenerateReviews(book, request.AvgReviews, random, region);
            return book;
        }
        private string GenerateISBN(Random random)
        {
            return $"{random.Next(900, 999)}-{random.Next(1, 9)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(0, 9)}";
        }
        private int CalculateFractionalCount(double value, Random random)
        {
            int floor = (int)Math.Floor(value);
            return floor + (random.NextDouble() < (value - floor) ? 1 : 0);
        }
        private List<Review> GenerateReviews(Book book, double avgReviews, Random random, FakerData region)
        {
            var reviews = new List<Review>();
            int reviewCount = CalculateFractionalCount(avgReviews, random);

            for (int i = 0; i < reviewCount; i++)
            {
                reviews.Add(new Review
                {
                    Text = region.ReviewTexts[random.Next(region.ReviewTexts.Length)],
                    Author = region.GetRandomAuthor(random),
                    Company = region.Companies[random.Next(region.Companies.Length)]
                });
            }
            return reviews;
        }
    private Book GenerateBook(GeneratorRequest request, RegionData region, Random random, int index)
    {
        if (request.Region == "de-DE")
        {
            var germanData = _regionData["de-DE"];
            string author = germanData.GetRandomAuthor(random);
        }
        return new Book
        {
            Author = region.GetRandomAuthor(random), 
        };
    }
    private string GenerateCoverUrl(string region, Random random)
        {
            var colors = new[] { "red", "blue", "green", "yellow" };
            return $"https://placeholder.com/{random.Next(300, 500)}x{random.Next(400, 600)}/{colors[random.Next(colors.Length)]}/FFFFFF?text={Uri.EscapeDataString("Cover")}";
        }
    }

}