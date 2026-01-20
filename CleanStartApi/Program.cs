var builder = WebApplication.CreateBuilder(args);

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

// Health
app.MapGet("/api/health", () => Results.Ok("OK"));

// Liste
app.MapGet("/api/products", () => Results.Ok(products));

// Tek ürün
app.MapGet("/api/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.Run();

record Product(int Id, string Name, decimal Price);
