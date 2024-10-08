using Catalog.API.Data;
using Catalog.API.Repositoies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x=>
{
   // x.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });

});

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x=>x.SwaggerEndpoint("/swagger/v1/swagger.json","Catalog.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
