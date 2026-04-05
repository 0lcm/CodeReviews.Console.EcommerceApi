using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public class ItemRepository(ApiDbContext db) : IItemRepository
{
    public async Task PostItemAsync(Item item)
    {
        db.Items.Add(item);
        await db.SaveChangesAsync();
    }

    public async Task<IQueryable<Item>> GetItemsAsync()
    {
        return db.Items
            .Include(i => i.Tags)
            .OrderBy(i => i.ItemId)
            .AsQueryable();
    }
}