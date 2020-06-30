using Database;
using Microsoft.EntityFrameworkCore;

namespace CreateAndSeedDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DataContext())
            {
                db.Database.EnsureDeleted();

                db.Database.Migrate();

                Seed.SeedData(db);
            }
        }
    }
}
