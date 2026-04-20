using ECommerce.UI.Enums;
using ECommerce.UI.Helpers;
using ECommerce.UI.Interfaces;
using static ECommerce.UI.Helpers.DisplayHelper;

namespace ECommerce.UI.AdministratorUi;

internal class ManageSalesUi(ISaleService saleService)
{
    //------- Menu Methods -------
    internal async Task ManageSales()
    {
        while (true)
        {
            var option = DisplayMenu<ManageSalesMenu>();

            switch (option)
            {
                case ManageSalesMenu.ReviewSales:
                    await ReviewSales();
                    break;
                case ManageSalesMenu.CreateNewSale:
                    //TODO add a call to the create sale method
                    break;
                case ManageSalesMenu.DeleteSale:
                    //TODO add a call to the delete sale method
                    break;
                case ManageSalesMenu.Back:
                    return;
            }
        }
    }
    
    //------- CRUD Menus -------
    private async Task ReviewSales()
    {
        var pageNumber = 1;
        while (true)
        {
            try
            {
                Console.Clear();
                var response = await saleService.GetSalesAsync(pageNumber);
                var iRenderable = UiHelper.BuildSaleDtoRenderable(response);
                
                
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
}