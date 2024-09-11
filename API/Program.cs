using API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureDependencies(builder.Configuration);
builder.Services.ConfigureLocalization();
builder.Services.ConfigureCors();
builder.Services.AddSwaggerGen(); // Swagger configuration
builder.Services.ConfigureOtherServices(); // Other services

var app = builder.Build();

// Configure middleware and services
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.SeedData(); // Seed initial data
app.UseMiddlewareConfigurations(); // Configure custom middleware
app.UseLocalization(); // Localization middleware
app.MapControllers(); // Map controllers to routes
await app.UpdateDatabase(); // Update the database schema

app.Run();