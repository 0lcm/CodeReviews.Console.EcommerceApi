namespace ECommerce.API.Models;

public class Sale
{
    public int SaleId { get; set; }
    public List<Item> SoldItems { get; } = [];
    
    public decimal TotalPrice => SoldItems.Sum(item => item.Price);
}