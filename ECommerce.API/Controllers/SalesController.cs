using ECommerce.API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(ISaleService saleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostSale([FromQuery] List<int> soldItemIds)
    {
        var success = await saleService.PostSaleAsync(itemIds: soldItemIds);
        if (success is null)
            return NotFound();
        if (success is false)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Created();
    }
}