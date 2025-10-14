using CWA_asian_store.Data;
using CWA_asian_store.Interfaces;
using CWA_asian_store.Middleware;
using CWA_asian_store.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Беремо connection string з appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Додаємо EF Core з SQL Server
builder.Services.AddDbContext<AsianFoodDbContext>(options => options.UseSqlServer(connection));

// Реєструємо сервіси
builder.Services.AddScoped<IProductService, ProductService>();

// Додаємо контролери
builder.Services.AddControllers();

var app = builder.Build();

// Додаємо middleware
app.UseMiddleware<RequestLogging>();

// Маршрутизація до контролерів
app.MapControllers();

app.Run();
