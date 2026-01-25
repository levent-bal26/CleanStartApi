using Microsoft.EntityFrameworkCore;
using CleanStartApi.Data;
var builder = WebApplication.CreateBuilder(args);
// ✅ Controller desteğini AÇ
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Demo data
var products = new[]
{
    new Product(1, "Kalem", 15),
    new Product(2, "Defter", 45),
    new Product(3, "Silgi", 5)
};

// Health (Minimal API)
app.MapGet("/api/health", () => Results.Ok("OK"));

// Liste (Minimal API)
app.MapGet("/api/products", () => Results.Ok(products));

// Tek ürün (Minimal API)
app.MapGet("/api/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

// ✅ Controller endpoint’lerini BAĞLA
app.MapControllers();

app.Run();

record Product(int Id, string Name, decimal Price);
