using Manufacturer.BaseWebApi.API;
using Manufacturer.Project.Core.DataContext;
using Microsoft.EntityFrameworkCore;

public static class HostExtensions
{
    public static IHost RunMigrations(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ProjectDBContext>();
                context.Database.Migrate(); // Aplica as migrations automaticamente
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }
        }

        return host;
    }
}
