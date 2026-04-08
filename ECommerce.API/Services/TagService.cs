using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using ECommerce.Shared.Models;

namespace ECommerce.API.Services;

public class TagService(ITagRepository repo) : ITagService
{
    public async Task PostTagAsync(CreateTagDto tagDto)
    {
        if (await repo.GetTagByName(tagDto.TagName) is not null)
        {
            return;
        }
        
        var tag = new Tag
        {
            TagName = tagDto.TagName
        };

        await repo.PostTagAsync(tag);
    }

    /// <summary>
    /// Deletes a tag asynchronously
    /// </summary>
    /// <returns>true upon a successful deletion, false upon an unsuccessful attempt, and null
    /// upon a NotFound exception.</returns>
    public async Task<bool?> DeleteTagByIdAsync(int id)
    {
        var tag = await repo.GetTagById(id);
        if (tag is null)
            return null;

        try
        {
            await repo.DeleteTagAsync(tag);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}