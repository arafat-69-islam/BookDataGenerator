using System.Text.Json;

namespace BookDataGenerator.Services
{
    public class DataLoaderService
    {
        private readonly IWebHostEnvironment _env;

        public DataLoaderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public T LoadJsonData<T>(string fileName)
        {
            var path = Path.Combine(_env.ContentRootPath, "Data", fileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Data file not found: {fileName}");
            }
            
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException($"Failed to deserialize {fileName}");
        }
    }
}
