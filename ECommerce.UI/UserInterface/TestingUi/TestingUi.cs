using ECommerce.UI.Enums;
using static ECommerce.UI.Helpers.DisplayHelper;

namespace ECommerce.UI.UserInterface.TestingUi;

internal class TestingUi(ProductTestUi testingUi)
{
    internal async Task TestingMenu()
    {
        while (true)
        {
            Console.Clear();
            
            var option = DisplayMenu<TestingMenuOption>();

            switch (option)
            {
                case TestingMenuOption.BrowseProducts:
                    await testingUi.ReviewProductsMenu();
                    break;
                case TestingMenuOption.SearchProducts:
                    await testingUi.SearchProducts();
                    break;
                case TestingMenuOption.Checkout:
                    //TODO add a call to the checkout method
                    break;
                case TestingMenuOption.ExitTestingEnvironment:
                    return;
            }
        }
    }
}