using Microsoft.EntityFrameworkCore;
using respTest1.Contexts;
using respTest1.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Data Source=houses.db";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


builder.Services.AddScoped<ApiHousesSeed>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Popular o banco de dados com dados da API
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var apiService = scope.ServiceProvider.GetRequiredService<ApiHousesSeed>();

    var apiUrl = "https://www.anapioficeandfire.com/api/houses";
    await apiService.PopulateDatabaseWithApiDataAsync(dbContext, apiUrl);
}



app.Run();

