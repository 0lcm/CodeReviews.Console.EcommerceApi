namespace ECommerce.API.Models;

public class Tag
{
    public int TagId { get; set; }
    public required string TagName {get; set;}
    public List<Item> Items { get; } = [];
}