using ECommerce.UI.Enums;
using ECommerce.UI.Helpers;
using Microsoft.Extensions.Hosting;
using static ECommerce.UI.Helpers.DisplayHelper;

namespace ECommerce.UI.AdministratorUi;

internal class AdministratorUi(ManageProductsUi manageProductsUi)
{
    //------- Main Menu Methods -------
    internal async Task MainMenu()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                var option = DisplayMenu<AdminMainMenu>();
                await HandleMainMenuOption(option);
            }
            catch (Exception ex)
            {
                UiHelper.DisplayCaughtException(ex);
            }
        }
    }

    private async Task HandleMainMenuOption(AdminMainMenu option)
    {
        switch (option)
        {
            case AdminMainMenu.ManageProducts:
                await manageProductsUi.ManageProductsMenu();
                break;
            case  AdminMainMenu.ManageProductTags:
                //TODO add a call to the Manage product tags menu
                break;
            case AdminMainMenu.ManageSales:
                //TODO add a call to the manage sales menu
                break;
            case AdminMainMenu.EnterTestingEnvironment:
                //TODO add a call to the test UI's main menu
                break;
            case AdminMainMenu.ExitApplication:
                     ExitApplication();
                break;
        }
    }

    private static void ExitApplication()
    {
        Environment.Exit(0);
    }
}