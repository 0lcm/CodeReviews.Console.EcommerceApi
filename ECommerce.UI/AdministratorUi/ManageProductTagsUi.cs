using static ECommerce.UI.Helpers.DisplayHelper;
using ECommerce.UI.Enums;
using ECommerce.UI.Interfaces;
using ECommerce.UI.Helpers;

namespace ECommerce.UI.AdministratorUi;

internal class ManageProductTagsUi(ITagService tagService)
{
    private readonly UiHelper _uiHelper = new UiHelper(tagService);
    
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
                    //TODO add a call to search tags
                    break;
                case ManageProductTagsMenu.CreateNewTag:
                    //TODO add a call to create tag
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
    /// <param name="searchGenre">optional genre filter</param>
    private async Task ReviewTags(string? searchTerm = null, string? searchGenre = null)
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await tagService.GetTagsAsync(pageNumber, searchTerm: searchTerm, searchGenre: searchGenre);
                var iRenderable = _uiHelper.BuildTagDtoRenderable(response);
                
                
                DisplayRows(iRenderable);
            
                var option = DisplayMenu<PaginationControlMenu>();
                switch (option)
                {
                    case PaginationControlMenu.LastPage:
                        pageNumber = pageNumber == 1 ? 1 :  pageNumber - 1;
                        break;
                    case PaginationControlMenu.NextPage:
                        pageNumber += 1;
                        break;
                    case PaginationControlMenu.Back:
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