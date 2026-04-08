using ECommerce.Shared.Models;

namespace ECommerce.API.Interfaces;

public interface ITagService
{
    public Task PostTagAsync(CreateTagDto tag);
    public Task<bool?> DeleteTagByIdAsync(int id);
}