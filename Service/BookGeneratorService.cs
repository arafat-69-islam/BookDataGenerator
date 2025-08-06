using BookDataGenerator.Models;
using BookDataGenerator.Services;

namespace BookDataGenerator.Services
{
    public class BookGeneratorService
    {
        private readonly DataLoaderService _dataLoader;

        public BookGeneratorService(DataLoaderService dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public List<Book> GenerateBooks(string language, int page, int pageSize, double avgLikes, double avgReviews, string seed)
        {
            var books = new List<Book>();
            var rngSeed = seed.GetHashCode() + page;
            var rng = new Random(rngSeed);

            var titles = _dataLoader.GetTitles(language);
            var authors = _dataLoader.GetAuthors(language);
            var publishers = _dataLoader.GetPublishers(language);

            for (int i = 0; i < pageSize; i++)
            {
                int index = (page - 1) * pageSize + i + 1;
                var title = titles[rng.Next(titles.Count)];
                var author = authors[rng.Next(authors.Count)];
                var publisher = publishers[rng.Next(publishers.Count)];
                // rng.Next max value is exclusive and int max is 2147483647, so 9999999999 is too large
                // Use NextInt64 for 10-digit number generation
                var isbn = rng.NextInt64(1000000000L, 9999999999L).ToString();
                int likes = (int)Math.Round(avgLikes);
                int reviewsCount = rng.NextDouble() < (avgReviews % 1) ? (int)avgReviews + 1 : (int)avgReviews;

                // Generate reviews
                var reviewDetails = new List<Review>();
                for (int r = 0; r < reviewsCount; r++)
                {
                    var reviewAuthor = authors[rng.Next(authors.Count)];
                    var reviewText = $"Review {r + 1} for {title} by {reviewAuthor}";
                    // Assuming reviewDetails is a List<Review> and Review class has Author and Text properties
                    reviewDetails.Add(new Review { Author = reviewAuthor ?? string.Empty, Text = reviewText ?? string.Empty });
                }

                // Generate cover image URL (using a placeholder service)
                var coverImageUrl = $"https://via.placeholder.com/200x300.png?text={Uri.EscapeDataString(title + " - " + author)}";

                books.Add(new Book
                {
                    Index = index,
                    ISBN = isbn,
                    Title = title,
                    Author = author,
                    Publisher = publisher,
                    Likes = likes,
                    Reviews = reviewsCount,
                    ReviewDetails = reviewDetails,
                    CoverImageUrl = coverImageUrl
                });
            }
            return books;
        }
    }
}
