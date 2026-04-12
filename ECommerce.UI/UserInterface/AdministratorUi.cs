using static ECommerce.UI.UserInterface.DisplayHelper;
using ECommerce.UI.Enums;

namespace ECommerce.UI.UserInterface;

internal class AdministratorUi
{
    //------- Main Menu Methods -------
    internal async Task MainMenu()
    {
        while (true)
        {
            var option = DisplayMenu<AdminMainMenu>();
            await HandleMainMenuOption(option);
        }
    }

    private async Task HandleMainMenuOption(AdminMainMenu option)
    {
        switch (option)
        {
            case AdminMainMenu.ManageProductsAndProductTags:
                //TODO add a call to the manage products menu
                break;
            case AdminMainMenu.ManageSales:
                //TODO add a call to the manage sales menu
                break;
            case AdminMainMenu.EnterTestingEnvironment:
                //TODO add a call to the test UI's main menu
                break;
            case AdminMainMenu.ExitApplication:
                await ExitApplication();
                break;
        }
    }

    private static async Task ExitApplication()
    {
        await DisplaySpinner("Exiting application...", 2500);
        Environment.Exit(0);
    }
}