using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using ECommerce.Shared.Models;
using ECommerce.UI.Configuration;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;
using ECommerce.UI.Interfaces.AdministratorInterfaces;

namespace ECommerce.UI.Services.AdministratorServices;

public class ManageProductsService(IApiService apiService) : IManageProductsService
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