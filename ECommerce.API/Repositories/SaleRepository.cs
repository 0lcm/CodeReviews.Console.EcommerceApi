using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;

namespace ECommerce.API.Repositories;

public class SaleRepository(ApiDbContext db) : ISaleRepository
{
    public async Task PostSale(Sale sale)
    {
        db.Sales.Add(sale);
        await db.SaveChangesAsync();
    }
}