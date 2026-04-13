using static ECommerce.UI.Helpers.DisplayHelper;
using ECommerce.UI.Enums;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;
using Spectre.Console.Rendering;

namespace ECommerce.UI.AdministratorUi;

internal class ManageProductsUi(IItemService productsService)
{
    //------- Menu Methods -------
    internal async Task ManageProductsMenu()
    {
        while (true)
        {
            Console.Clear();
            var option = DisplayMenu<ManageProductsMenuOption>();

            switch (option)
            {
                case ManageProductsMenuOption.ReviewProducts:
                    await ReviewProductsMenu();
                    break;
                case ManageProductsMenuOption.SearchProducts:
                    //TODO add a call to the search products menu
                    break;
                case ManageProductsMenuOption.CreateNewProduct:
                    //TODO add a call to the create new product menu
                    break;
                case ManageProductsMenuOption.EditProduct:
                    //TODO add a call to the edit product menu
                    break;
                case ManageProductsMenuOption.DeleteProduct:
                    //TODO add a call to the delete product menu
                    break;
                case ManageProductsMenuOption.Back:
                    return;
            }
        }
    }
    
    //------- CRUD Menus -------
    private async Task ReviewProductsMenu()
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await productsService.GetItemsAsync(pageNumber);
                var iRenderable = UiHelper.BuildItemDtoRenderable(response);
                
                
                DisplayRows(iRenderable);
            
                var option = DisplayMenu<Enums.ReviewProductsMenu>();
                switch (option)
                {
                    case Enums.ReviewProductsMenu.LastPage:
                        pageNumber = pageNumber == 1 ? 1 :  pageNumber - 1;
                        break;
                    case Enums.ReviewProductsMenu.NextPage:
                        pageNumber += 1;
                        break;
                    case Enums.ReviewProductsMenu.Back:
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