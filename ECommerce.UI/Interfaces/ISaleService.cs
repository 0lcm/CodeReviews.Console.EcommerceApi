using ECommerce.Shared.Models;

namespace ECommerce.UI.Interfaces;

public interface ISaleService
{
    public Task<PagedResponse<SaleDto>> GetSalesAsync(int pageNumber = 1, int pageSize = 10);
}