namespace API.Configurations;

public static class LocalizationConfiguration
{
    public static void ConfigureLocalization(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "";
        });

        services.Configure<RequestLocalizationOptions>(options =>
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
    }
    
    public static void UseLocalization(this WebApplication app)
    {
        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(options!.Value);
    }

}
