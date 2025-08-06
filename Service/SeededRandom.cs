namespace BookDataGenerator.Services
{
    public class SeededRandom
    {
        private int seed;
        
        public SeededRandom(int initialSeed)
        {
            seed = initialSeed;
        }

        public double Next()
        {
            seed = (seed * 9301 + 49297) % 233280;
            return (double)seed / 233280;
        }

        public int NextInt(int min, int max)
        {
            return (int)Math.Floor(Next() * (max - min + 1)) + min;
        }

        public double NextDouble(double min, double max)
        {
            return Next() * (max - min) + min;
        }

        public T Choice<T>(T[] array)
        {
            return array[NextInt(0, array.Length - 1)];
        }
    }
}
