using static ECommerce.UI.Helpers.DisplayHelper;
using ECommerce.UI.Enums;
using ECommerce.UI.Interfaces;
using ECommerce.UI.Helpers;
using Spectre.Console;

namespace ECommerce.UI.AdministratorUi;

internal class ManageProductTagsUi(ITagService tagService)
{
    private readonly UiHelper _uiHelper = new(tagService);
    
    //------- Menu Methods -------
    internal async Task ManageProductTags()
    {
        while (true)
        {
            Console.Clear();

            var option = DisplayMenu<ManageProductTagsMenu>();

            switch (option)
            {
                case ManageProductTagsMenu.ReviewTags:
                    await ReviewTags();
                    break;
                case ManageProductTagsMenu.SearchTags:
                    await SearchTags();
                    break;
                case ManageProductTagsMenu.CreateNewTag:
                    await CreateNewTag();
                    break;
                case ManageProductTagsMenu.DeleteTag:
                    //TODO add a call to delete tag
                    break;
                case ManageProductTagsMenu.Back:
                    return;
            }
        }
    }
    
    //------- CRUD Menus -------
    /// <summary>
    /// Presents a list of tags to the user and handles pagination
    /// </summary>
    /// <param name="searchTerm">optional search term to filter results</param>
    private async Task ReviewTags(string? searchTerm = null)
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await tagService.GetTagsAsync(pageNumber, searchTerm: searchTerm);
                var iRenderable = _uiHelper.BuildTagDtoRenderable(response);
                
                
                DisplayRows(iRenderable);
            
                var option = DisplayMenu<PaginationController>();
                switch (option)
                {
                    case PaginationController.LastPage:
                        pageNumber = pageNumber == 1 ? 1 :  pageNumber - 1;
                        break;
                    case PaginationController.NextPage:
                        pageNumber += 1;
                        break;
                    case PaginationController.Back:
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
    
    private async Task SearchTags()
    {
        while (true)
        {
            Console.Clear();
    
            var searchTerm = UiHelper.GetArgument("Please enter a term to search by:");
            if (searchTerm is null) return;
            
            await ReviewTags(searchTerm);
    
            if (!await AnsiConsole.ConfirmAsync("Would you like to perform another search?"))
                return;
        }
    }
    
    private async Task CreateNewTag()
    {
        while (true)
        {
            var name = UiHelper.GetArgument("Please enter a name for the new tag:");
            if (name is null) return;

            if (!await AnsiConsole.ConfirmAsync($"are you sure you want to create a new tag with the name {name}?"))
                continue;

            try
            {
                await tagService.PostTagAsync(name);
                DisplaySuccess("Successfully  created a new tag");
                UiHelper.WaitForUser();
            }
            catch (HttpRequestException ex)
            {
                UiHelper.DisplayCaughtException(ex);
            }

            return;
        }
    }
}