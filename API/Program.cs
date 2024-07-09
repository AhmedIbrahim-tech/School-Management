using Data.Entities.Identities;
using Infrastructure.Context;
using Infrastructure.Context.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

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

builder.Services.AddInfrastructureDependencies().AddServicesDependencies().AddCoreDependencies().AddServiceRegisteration(builder.Configuration);

#endregion

#region Localization

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder =>
        {
            builder
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowAnyOrigin();
        });
});
#endregion

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});


var app = builder.Build();

#region Create Data Seed

using (var Seed = app.Services.CreateScope())
{
    var userManager = Seed.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = Seed.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region Localization Middle ware

var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(option.Value);

#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

#region Authorize
app.UseAuthentication();
app.UseAuthorization();
#endregion

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

#region Create Data Seed

using (var Seed = app.Services.CreateScope())
{
    var roleManager = Seed.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = Seed.ServiceProvider.GetRequiredService<UserManager<User>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

#endregion


app.Run();
