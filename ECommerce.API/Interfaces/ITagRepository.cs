using ECommerce.API.Models;

namespace ECommerce.API.Interfaces;

public interface ITagRepository
{
    public Task<Tag?> GetTagByName(string name);
    public Task PostTagAsync(Tag tag);
}