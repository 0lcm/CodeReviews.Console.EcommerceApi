using static ECommerce.UI.Helpers.DisplayHelper;
using ECommerce.UI.Enums;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;
using Spectre.Console;
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
                    await SearchProducts();
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
    /// <summary>
    /// Presents a list of products to the user and handles pagination
    /// </summary>
    /// <param name="searchTerm">optional search term to filter results</param>
    /// <param name="searchGenre">optional genre filter</param>
    private async Task ReviewProductsMenu(string? searchTerm = null, string? searchGenre = null)
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await productsService.GetItemsAsync(pageNumber, searchTerm: searchTerm, searchGenre: searchGenre);
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

    private async Task SearchProducts()
    {
        while (true)
        {
            Console.Clear();
            
            List<string> enumNames = Enum.GetNames<SearchProductsMenu>().ToList();
            var options = DisplayMultiPrompt(enumNames);

            List<SearchProductsMenu> selectedFilters;
            try
            {
                selectedFilters = options
                    .Select(s => (SearchProductsMenu)Enum.Parse<SearchProductsMenu>(s))
                    .ToList();
            }
            catch (Exception ex)
            {
                DisplayWarning("Please select to either search by a given search term, or filter by genre.");
                UiHelper.WaitForUser();
                continue;
            }

            string? searchTerm = null;
            string? searchGenre = null;

            if (selectedFilters.Contains(SearchProductsMenu.SearchByTerm))
            {
                Console.Clear();
                searchTerm = DisplayQuestion("Please enter a term to search for:");
            }

            if (selectedFilters.Contains(SearchProductsMenu.FilterByGenre))
            {
                Console.Clear();
                searchGenre = DisplayQuestion("Please enter a genre to filter by:");
            }

            await ReviewProductsMenu(searchTerm, searchGenre);

            if (!await AnsiConsole.ConfirmAsync("Would you like to perform another search?"))
                return;
        }
    }
}