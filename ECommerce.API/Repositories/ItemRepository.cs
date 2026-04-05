using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;

namespace ECommerce.API.Repositories;

public class ItemRepository(ApiDbContext db) : IItemRepository
{
    public async Task PostItemAsync(Item item)
    {
        db.Items.Add(item);
        await db.SaveChangesAsync();
    }
}