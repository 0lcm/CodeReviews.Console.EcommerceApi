using ECommerce.API.Interfaces;
using ECommerce.API.Models;

namespace ECommerce.API.Services;

public class SaleService(ISaleRepository repo, IItemRepository itemRepo) : ISaleService
{
    /// <summary>
    /// Posts a sale async
    /// </summary>
    /// <param name="itemIds"></param>
    /// <returns>True upon a successful post, false upon an unsuccessful post, and null upon a NotFound error.</returns>
    public async Task<bool?> PostSaleAsync(List<int> itemIds)
    {
        var items = new List<Item>();
        foreach (var id in itemIds)
        {
            var item = await itemRepo.GetItemByIdAsync(id);
            if (item is null)
                return null;
        }

        var sale = new Sale
        {
            SoldItems = items
        };

        try
        {
            await repo.PostSale(sale);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}