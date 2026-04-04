using SportsStatistics.Application;
using SportsStatistics.Infrastructure;
using SportsStatistics.Web;
using SportsStatistics.Web.Api.Endpoints;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.AddApplication()
               .AddPresentation()
               .AddInfrastructure();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        var isDevelopment = app.Environment.IsDevelopment();

        app.MapApiEndpoints(isDevelopment);

        if (!isDevelopment)
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        await app.RunAsync();
    }
}