namespace BookDataGenerator.Services
{
    public class SeedService
    {
        public int GenerateRandomSeed()
        {
            return new Random().Next(1, int.MaxValue);
        }

        public int CombineSeedWithPage(int seed, int page)
        {
            unchecked
            {
                return seed + page * 397;
            }
        }
    }
}
