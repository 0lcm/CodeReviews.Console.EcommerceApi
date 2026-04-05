using ECommerce.API.Models;

namespace ECommerce.API.Interfaces;

public interface IItemRepository
{
    public Task PostItemAsync(Item item);
}