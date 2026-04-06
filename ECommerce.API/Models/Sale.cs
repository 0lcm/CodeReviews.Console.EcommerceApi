using ECommerce.API.Interfaces;

namespace ECommerce.API.Models;

public class Sale : ISoftDeletable
{
    public int SaleId { get; set; }
    public List<Item> SoldItems { get; set; } = [];
    
    public decimal TotalPrice => SoldItems.Sum(item => item.Price);
    
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}