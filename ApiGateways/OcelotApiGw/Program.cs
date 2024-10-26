using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot services to the DI container
builder.Services.AddOcelot();

// Load Ocelot configuration based on the environment
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Configure logging
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Use Ocelot middleware to handle routing
await app.UseOcelot();

// Map a simple health check route
app.MapGet("/", () => "Hello World!");

// Run the application
app.Run();
