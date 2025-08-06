using BookDataGenerator.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace BookDataGenerator.Services
{
    public class BookGeneratorService
    {
        private readonly Dictionary<string, RegionData> _regionData;
        private readonly DataLoaderService _dataLoader;
        private readonly ILogger<BookGeneratorService> _logger;

        public BookGeneratorService(DataLoaderService dataLoader, ILogger<BookGeneratorService> logger)
        {
            _dataLoader = dataLoader;
            _logger = logger;
            _regionData = LoadAllRegionData();
        }

        private Dictionary<string, RegionData> LoadAllRegionData()
        {
            try
            {
                var bookTitles = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("BookTitles.json");
                var names = _dataLoader.LoadJsonData<Dictionary<string, Dictionary<string, string[]>>>("Names.json");
                var publishers = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("Publishers.json");
                var reviewData = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("ReviewData.json");

                var regions = new Dictionary<string, RegionData>();
                var supportedRegions = new[] { "en-US", "fr-FR", "de-DE", "es-ES", "ja-JP" };

                foreach (var region in supportedRegions)
                {
                    regions[region] = new RegionData
                    {
                        Titles = bookTitles.ContainsKey(region) ? bookTitles[region] : bookTitles["en-US"],
                        FirstNames = names["firstNames"].ContainsKey(region) ? names["firstNames"][region] : names["firstNames"]["en-US"],
                        LastNames = names["lastNames"].ContainsKey(region) ? names["lastNames"][region] : names["lastNames"]["en-US"],
                        Publishers = publishers.ContainsKey(region) ? publishers[region] : publishers["en-US"],
                        ReviewTexts = reviewData["reviewTexts"],
                        Companies = reviewData["companies"]
                    };
                }

                return regions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading region data");
                throw;
            }
        }

        public List<Book> GenerateBooks(GeneratorRequest request, int count = 20)
        {
            try
            {
                int seed = request.Seed ?? 42;
                int page = request.Page ?? 1;
                var combinedSeed = seed + page;

                if (string.IsNullOrEmpty(request.Region))
                    request.Region = "en-US";

                if (!_regionData.TryGetValue(request.Region, out var region))
                    throw new ArgumentException($"Unsupported region: {request.Region}");

                var books = new List<Book>();
                for (int i = 0; i < count; i++)
                {
                    var bookRandom = new SeededRandom(combinedSeed + i);
                    books.Add(GenerateSingleBook(request, region, bookRandom, i));
                }
                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating books with request {@Request}", request);
                throw;
            }
        }

        private Book GenerateSingleBook(GeneratorRequest request, RegionData region, SeededRandom random, int index)
        {
            int page = request.Page ?? 1;
            double avgLikes = request.AvgLikes ?? 0.0;
            double avgReviews = request.AvgReviews ?? 0.0;
            string regionStr = string.IsNullOrEmpty(request.Region) ? "en-US" : request.Region;

            var book = new Book
            {
                Index = (page - 1) * 20 + index + 1,
                ISBN = GenerateISBN(random),
                Title = random.Choice(region.Titles),
                Author = region.GetRandomAuthor(random),
                Publisher = random.Choice(region.Publishers),
                Likes = CalculateFractionalCount(avgLikes, random),
                Region = regionStr,
                CoverImageUrl = GenerateCoverUrl(regionStr, random)
            };

            book.Reviews = GenerateReviews(avgReviews, random, region);
            return book;
        }

        private string GenerateISBN(SeededRandom random)
        {
            return $"{random.NextInt(900, 999)}-{random.NextInt(1, 9)}-{random.NextInt(10, 99)}-{random.NextInt(100000, 999999)}-{random.NextInt(0, 9)}";
        }

        private int CalculateFractionalCount(double value, SeededRandom random)
        {
            int floor = (int)Math.Floor(value);
            double fraction = value - floor;
            return floor + (random.NextDouble(0.0, 1.0) < fraction ? 1 : 0);
        }

        private List<Review> GenerateReviews(double avgReviews, SeededRandom random, RegionData region)
        {
            var reviews = new List<Review>();
            int reviewCount = CalculateFractionalCount(avgReviews, random);

            for (int i = 0; i < reviewCount; i++)
            {
                var reviewRandom = new SeededRandom(random.NextInt(1, 10000));
                reviews.Add(new Review
                {
                    Text = reviewRandom.Choice(region.ReviewTexts),
                    Author = region.GetRandomAuthor(reviewRandom),
                    Company = reviewRandom.Choice(region.Companies)
                });
            }
            return reviews;
        }

        private string GenerateCoverUrl(string region, SeededRandom random)
        {
            var colors = new[] { "red", "blue", "green", "yellow", "purple", "orange" };
            var color = random.Choice(colors);
            var width = random.NextInt(300, 500);
            var height = random.NextInt(400, 600);
            return $"https://via.placeholder.com/{width}x{height}/{color}/FFFFFF?text=Cover";
        }
    }
}
