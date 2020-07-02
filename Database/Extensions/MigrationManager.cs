using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Database.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                {
                    if (appContext.Database.EnsureCreated())
                    {
                        appContext.Database.Migrate();
                    }

                    Seed.SeedData(appContext);
                }
            }

            return host;
        }
    }
}
