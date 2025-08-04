using BookDataGenerator.Services;
builder.Services.AddSingleton<DataLoaderService>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BookGeneratorService>();
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<DataExportService>(); 
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<DataLoaderService>();
builder.Services.AddSingleton<BookGeneratorService>();
builder.Services.AddSingleton<SeedService>();
builder.Services.AddSingleton<DataExportService>(); // Optional

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Example usage in BookGeneratorService:
public class BookGeneratorService
{
    private readonly DataLoaderService _dataLoader;
    private readonly Dictionary<string, RegionData> _regionData;

    public BookGeneratorService(DataLoaderService dataLoader)
    {
        _dataLoader = dataLoader;
        _regionData = new Dictionary<string, RegionData>
        {
            ["en-US"] = LoadRegionData("en-US"),
            // ... other regions
        };
    }

    private RegionData LoadRegionData(string region)
    {
        return new RegionData
        {
            Titles = _dataLoader.LoadJsonData<Dictionary<string, string[]>>("BookTitles.json")[region],
            // ... load other data
        };
    }
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
