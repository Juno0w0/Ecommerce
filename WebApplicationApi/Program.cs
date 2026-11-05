using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Data;
using WebApplicationApi.Repository;
using WebApplicationApi.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agrega el DbContext al contenedor de servicios y configura la cadena de conexión a la base de datos
var dbConnectionString = builder.Configuration.GetConnectionString("ConnectionSQL");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
