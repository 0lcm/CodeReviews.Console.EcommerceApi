using ECommerce.UI.Enums;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;
using static ECommerce.UI.Helpers.DisplayHelper;

namespace ECommerce.UI.UserInterface.TestingUi;

internal class ProductTestUi(IItemService itemService, CheckoutUi checkoutUi)
{
    /// <summary>
    /// Presents a list of products to the user and handles pagination
    /// </summary>
    /// <param name="searchTerm">optional search term to filter results</param>
    /// <param name="searchGenre">optional genre filter</param>
    internal async Task ReviewProductsMenu(string? searchTerm = null, string? searchGenre = null)
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await itemService.GetItemsAsync(pageNumber, searchTerm: searchTerm, searchGenre: searchGenre);
                var iRenderable = UiHelper.BuildItemDtoRenderable(response);
                
                
                DisplayRows(iRenderable);
            
                var option = DisplayMenu<PaginationControllerWithAddToCart>();
                switch (option)
                {
                    case PaginationControllerWithAddToCart.LastPage:
                        pageNumber = pageNumber == 1 ? 1 :  pageNumber - 1;
                        break;
                    case PaginationControllerWithAddToCart.NextPage:
                        pageNumber += 1;
                        break;
                    case PaginationControllerWithAddToCart.AddToCart:
                        await checkoutUi.AddItemToCartAsync(response);
                        break;
                    case PaginationControllerWithAddToCart.Back:
                        return;
                }
            }
            catch (HttpRequestException ex)
            {
                UiHelper.DisplayCaughtException(ex);
                return;
            }
        }
    }
}