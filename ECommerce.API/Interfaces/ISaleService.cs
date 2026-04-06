namespace ECommerce.API.Interfaces;

public interface ISaleService
{
    public Task<bool?> PostSaleAsync(List<int> itemIds);
}