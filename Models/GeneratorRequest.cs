namespace BookDataGenerator.Models
{
    public class GeneratorRequest
    {
        public string? Region { get; set; }
        public int? Seed { get; set; }
        public double? AvgLikes { get; set; }
        public double? AvgReviews { get; set; }
        public int? Page { get; set; }
    }
}
