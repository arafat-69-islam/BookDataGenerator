namespace BookDataGenerator.Models
{
    public class Review
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }

    public class Book
    {
        public int Index { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Reviews { get; set; }
        public List<Review> ReviewDetails { get; set; } // Add this
        public string CoverImageUrl { get; set; } // Add this
    }
}
