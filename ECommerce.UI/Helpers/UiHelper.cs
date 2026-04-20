using ECommerce.Shared.Models;
using ECommerce.UI.Interfaces;
using Spectre.Console;
using Spectre.Console.Rendering;
using static ECommerce.UI.Helpers.DisplayHelper;

namespace ECommerce.UI.Helpers;

internal class UiHelper(ITagService tagService)
{
    internal static List<IRenderable> BuildItemDtoRenderable(PagedResponse<ItemDto> response)
    {
        List<IRenderable> iRenderable = [];

        foreach (var item in response.Data)
        {
            iRenderable.Add(new Markup($"[{White}]\nTitle: [/][{Green}]{item.Name}[/]"));
            iRenderable.Add(new Markup($"[{White}]Artist: [/][{Grey}]{item.Artist}[/]"));
            iRenderable.Add(new Markup($"[{White}]Type / Format: [/][{Grey}]{item.Type} / {item.Format}[/]"));
            iRenderable.Add(new Markup($"[{White}]Genre: [/][{Grey}]{item.Genre}[/]"));
            iRenderable.Add(new Markup($"[{White}]Tags: [/][{Grey}]{string.Join(", ", item.Tags.Select(t => t.TagName))}[/]"));
            iRenderable.Add(new Markup($"[{White}]Price: [/][{Grey}]{item.Price}[/]"));
            iRenderable.Add(new Markup($"[{White}]Item ID: [/][{Yellow}]{item.ItemId}[/]"));
        }
        
        return iRenderable;
    }
    internal List<IRenderable> BuildTagDtoRenderable(PagedResponse<TagDto> response)
    {
        List<IRenderable> iRenderable = [];

        foreach (var tag in response.Data)
        {
            var tagId = tagService.GetTagIdByNameAsync(tag.TagName).Result;
            iRenderable.Add(new Markup($"[{White}]\nTag ID: [/][{Green}]{tagId}[/]"));
            iRenderable.Add(new Markup($"[{White}]Title: [/][{Grey}]{tag.TagName}[/]"));
        }
        
        return iRenderable;
    }
    
    internal static List<IRenderable> BuildSaleDtoRenderable(PagedResponse<SaleDto> response)
    {
        List<IRenderable> iRenderable = [];

        foreach (var sale in response.Data)
        {
            iRenderable.Add(new Markup($"[{White}]\nID: [/][{Green}]{sale.SaleId}[/]"));
            iRenderable.Add(new Markup($"[{White}]Items: [/][{Grey}]{string.Join(',', sale.SoldItems.Select(s => $"{s.Item.Name} x{s.Quantity}"))}[/]"));
            iRenderable.Add(new Markup($"[{White}]Total price: [/][{Grey}]{sale.TotalPrice}[/]"));
        }
        
        return iRenderable;
    }

    /// <summary>
    /// Displays a caught exception to the user based on the type of exception caught.
    /// Handles HttpResponseExceptions for status codes 404, 400-499, and 500+.
    /// Handles ArgumentNull and generic Argument exceptions.
    /// All other exceptions are labelled with a generic 'unexpected exception' label.
    /// </summary>
    /// <param name="ex">Caught exception</param>
    internal static void DisplayCaughtException(Exception ex)
    {
        Console.Clear();

        if (ex is HttpRequestException { StatusCode: not null } exception)
        {
            switch ((int)exception.StatusCode)
            {
                case var code when code is 404:
                    DisplayWarning("The content you requested could not be found, please check that the" +
                                   "content you want exists and is accessible. | 404 Not Found");
                    break;
                
                case var code when code is >= 400 and < 500:
                    DisplayWarning("A client side error has occurred while processing your request, " +
                                   "please check that you are sending a good request to the API containing valid details. | 4xx");
                    break;
                
                case var code when code is > 500:
                    DisplayWarning("A server side error has occurred while processing your request," +
                                   "please check that the API is working and all entered details are correct. | 5xx");
                    break;
                default:
                    DisplayWarning("An unknown error has occurred while attempting to interact with the API. | ???");
                    break;
            }
        }
        else if (ex is ArgumentException argException)
        {
            if (argException is ArgumentNullException)
                DisplayWarning("One or more of the arguments you have entered was null, " +
                               "please try again with non-null details.");
            else 
                DisplayWarning("An error has occurred with one or more of the arguments you have entered," +
                               "please check that any details you enter are correct before trying again.");
        }
        else
        {
            DisplayWarning("An unexpected error has occurred during runtime, " +
                           "please retry later or report the problem if it persists.");
        }
        
        WaitForUser();
    }

    internal static void WaitForUser()
    {
        DisplayMessage("Please press enter to continue.");
        Console.ReadLine();
    }

    /// <summary>
    /// Gets an argument from the user
    /// </summary>
    /// <param name="prompt">Display prompt shown when prompting the user</param>
    /// <param name="instructions">Any extra instructions, E.G asking for a specific format.</param>
    /// <returns>null if the user wishes to exit the process, or else the value returned form the prompt.</returns>
    internal static string? GetArgument(string prompt, string? instructions = null)
    {
        const string backOption = "back";
        
        Console.Clear();
        DisplayInfo("Enter 'Back' to leave this menu.");
        if (instructions != null) DisplayInfo(instructions);

        var value = DisplayQuestion(prompt);
        
        return string.Equals(value, backOption, StringComparison.OrdinalIgnoreCase) ? null : value;
    }
}