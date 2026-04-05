using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using ECommerce.Shared.Models;

namespace ECommerce.API.Services;

public class ItemService(IItemRepository repo, ITagRepository tagRepo) : IItemService
{
    public async Task PostItemAsync(CreateItemDto itemDto)
    {
        var tags = new List<Tag>();

        foreach (var tagDto in itemDto.Tags)
        {
            var existingTag = await tagRepo.GetTagByName(tagDto.TagName);
            tags.Add(existingTag ?? new Tag{TagName = tagDto.TagName});
        }
        
        var item = new Item
        {
            Format = itemDto.Format,
            Type = itemDto.Type,
            Name = itemDto.Name,
            Artist = itemDto.Artist,
            Genre = itemDto.Genre,
            Tags = tags,
            Price = itemDto.Price,
        };

        await repo.PostItemAsync(item);
    }
}