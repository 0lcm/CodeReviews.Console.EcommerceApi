using ECommerce.API.Services;

namespace ECommerce.API.Data;

public class DbSeeder
{
    public static async Task SeedDatabaseAsync(ApiDbContext db, ImportServiceFactory serviceFactory)
    {
        var importService = serviceFactory.Create();
        var seedData = importService.GetSeedData();
        await db.Tags.AddRangeAsync(seedData.Tags);
        await db.Items.AddRangeAsync(seedData.Items);
        await db.Sales.AddRangeAsync(seedData.Sales);
        await db.SaveChangesAsync();
    }
}