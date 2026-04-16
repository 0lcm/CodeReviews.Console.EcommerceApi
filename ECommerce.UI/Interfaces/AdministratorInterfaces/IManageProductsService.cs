using ECommerce.Shared.Models;

namespace ECommerce.UI.Interfaces.AdministratorInterfaces;

public interface IManageProductsService
{
    public Task<PagedResponse<ItemDto>> GetItemsAsync(int pageNumber = 1, int pageSize = 10, 
        string?  searchTerm = null, string? searchGenre = null);
}