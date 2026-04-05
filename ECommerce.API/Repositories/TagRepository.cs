using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public class TagRepository(ApiDbContext db) : ITagRepository
{
    public async Task<Tag?> GetTagByName(string name)
    {
        return await db.Tags.FirstOrDefaultAsync(t => t.TagName ==  name);
    }
}