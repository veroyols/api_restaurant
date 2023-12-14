using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{

    public class InitDb
    {
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using var serverScope = app.ApplicationServices.CreateScope();
            await SeedData(serverScope.ServiceProvider.GetService<AppDbContext>());

        }

        private static async Task SeedData(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }

        }
    }
}
