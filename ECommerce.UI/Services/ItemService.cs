using System.Text.Json;
using ECommerce.Shared.Models;
using ECommerce.UI.Configuration;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;

namespace ECommerce.UI.Services;

public class ItemService(IApiService apiService) : IItemService
{
    public async Task<PagedResponse<ItemDto>> GetItemsAsync(int pageNumber = 1, int pageSize = 10, 
        string? searchTerm = null, string? searchGenre = null)
    {
        var requestUrl = Utils.FormatQueryWithPaginationParams(baseUrl: ApiUris.ItemRequestUri,
            pageNumber, pageSize, searchTerm, searchGenre);

        var rawJson = await apiService.GetAsync(requestUrl);
        return JsonSerializer.Deserialize<PagedResponse<ItemDto>>(rawJson, Utils.GetJsonSerializerOptions())!;
    }
}