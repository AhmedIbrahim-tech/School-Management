using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureOtherServices(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddTransient<IUrlHelper>(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext);
        });
    }
}
