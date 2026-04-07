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
}