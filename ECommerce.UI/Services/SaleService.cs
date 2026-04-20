using System.Text.Json;
using ECommerce.Shared.Models;
using ECommerce.UI.Configuration;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;

namespace ECommerce.UI.Services;

public class SaleService(IApiService apiService) : ISaleService
{
    public async Task<PagedResponse<SaleDto>> GetSalesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var requestUrl = Utils.FormatQueryWithPaginationParams(ApiUris.SaleRequestUri, pageNumber, pageSize, null, null);
        var rawJson = await apiService.GetAsync(requestUrl);

        return JsonSerializer.Deserialize<PagedResponse<SaleDto>>(rawJson, Utils.GetJsonSerializerOptions())!;
    }
}