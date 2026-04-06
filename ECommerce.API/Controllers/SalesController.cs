using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using ECommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(ISaleService saleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostSale([FromBody]List<CreateSaleItemDto> saleItems)
    {
        var success = await saleService.PostSaleAsync(saleItems);
        if (success is null)
            return NotFound();
        if (success is false)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Created();
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResponse<Sale>>> GetItemsAsync([FromQuery] PaginationParams paginationParams)
    {
        var pagedResponse = await  saleService.GetSalesAsync(paginationParams);
        if (pagedResponse.TotalRecords == 0)
        {
            return NotFound();
        }
        return Ok(pagedResponse);
    }
}