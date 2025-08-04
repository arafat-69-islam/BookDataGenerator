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
            var json = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}