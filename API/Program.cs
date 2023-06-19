var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Connection Database

builder.Services.AddDbContext<ApplicationDBContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

#endregion

#region Dependency Injection

builder.Services.AddInfrastructureDependencies().AddServicesDependencies().AddCoreDependencies();

#endregion

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

#region Update Database

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<ApplicationDBContext>();
var logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    //await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error Occurred While Migrating Process");
}

#endregion

app.Run();
