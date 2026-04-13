using ECommerce.Shared.Models;

namespace ECommerce.UI.Interfaces;

public interface IItemService
{
    public Task<PagedResponse<ItemDto>> GetItemsAsync(int pageNumber = 1, int pageSize = 10, 
        string?  searchTerm = null, string? searchGenre = null);
}