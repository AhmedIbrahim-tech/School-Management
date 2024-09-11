namespace API.Configurations;

public static class MiddlewareConfiguration
{
    public static void UseMiddlewareConfigurations(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
