using static ECommerce.UI.Helpers.DisplayHelper;

using ECommerce.Shared.Models;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;

namespace ECommerce.UI.UserInterface.TestingUi;

internal class CheckoutUi(ICartService cartService)
{
    internal async Task AddItemToCartAsync(PagedResponse<ItemDto> response)
    {
        Console.Clear();

        Dictionary<string, ItemDto> items = new();
        foreach (var item in response.Data)
        {
            items.Add($"{item.Name} - {item.Artist} - ${item.Price}", item);
        }
        
        var options = DisplayMultiPrompt(items.Keys.ToList(), requireChoice: false);

        if (options.Count > 0)
        {
            foreach (var option in options)
            {
                await cartService.AddToCartAsync(items[option]);
            }
        }
        
        DisplaySuccess("Successfully added items to cart.");
        UiHelper.WaitForUser();
    }
}