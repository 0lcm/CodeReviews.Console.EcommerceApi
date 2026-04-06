using ECommerce.API.Data;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public class SaleRepository(ApiDbContext db) : ISaleRepository
{
    public async Task PostSale(Sale sale)
    {
        db.Sales.Add(sale);
        await db.SaveChangesAsync();
    }

    public IQueryable<Sale> GetSales()
    {
        return db.Sales
            .Include(s => s.SoldItems)
            .OrderBy(s => s.SaleId)
            .AsQueryable();
    }
}