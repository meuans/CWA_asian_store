using CWA_asian_store.Data;
using CWA_asian_store.Interfaces;
using CWA_asian_store.Middleware;
using CWA_asian_store.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Додаю EF Core з SQL Server
builder.Services.AddDbContext<AsianFoodDbContext>(options => options.UseSqlServer(connection));

// Реєструю сервіси
builder.Services.AddScoped<IProductService, ProductService>();

// Додаю контролер
//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


var app = builder.Build();


// Додаю middleware
app.UseMiddleware<RequestLogging>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");


app.Run();
