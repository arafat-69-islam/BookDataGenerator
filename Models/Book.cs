namespace BookDataGenerator.Models
{
    public class Book
    {
        public int Index { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Likes { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}