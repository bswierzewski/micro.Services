using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace JsonDb.Extensions
{
    public static class MigrationManager
    {
        public static void CreateAndSeedDatabase()
        {
            using (var appContext = new DataContext())
            {
                appContext.Database.EnsureDeleted();

                if (appContext.Database.GetPendingMigrations().Any())
                    appContext.Database.Migrate();

                Seed.SeedData(appContext);
            }
        }
    }
}
