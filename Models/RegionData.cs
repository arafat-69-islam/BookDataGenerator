using BookDataGenerator.Services;

namespace BookDataGenerator.Models
{
    public class RegionData
    {
        public string[] Titles { get; set; } = Array.Empty<string>();
        public string[] FirstNames { get; set; } = Array.Empty<string>();
        public string[] LastNames { get; set; } = Array.Empty<string>();
        public string[] Publishers { get; set; } = Array.Empty<string>();
        public string[] ReviewTexts { get; set; } = Array.Empty<string>();
        public string[] Companies { get; set; } = Array.Empty<string>();

        public string GetRandomAuthor(SeededRandom random)
        {
            var firstName = FirstNames[random.NextInt(0, FirstNames.Length - 1)];
            var lastName = LastNames[random.NextInt(0, LastNames.Length - 1)];
            return $"{firstName} {lastName}";
        }
    }
}
