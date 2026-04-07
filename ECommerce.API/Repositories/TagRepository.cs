using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public class TagRepository(ApiDbContext db) : ITagRepository
{
    public async Task<Tag?> GetTagByName(string name)
    {
        return await db.Tags.FirstOrDefaultAsync(t => string.Equals(t.TagName, name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task PostTagAsync(Tag tag)
    {
        db.Tags.Add(tag);
        await db.SaveChangesAsync();
    }
}