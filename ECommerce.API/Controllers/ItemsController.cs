using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using ECommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController(IItemService itemService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostItem([FromBody] CreateItemDto itemDto)
    {
        await itemService.PostItemAsync(itemDto);
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Item>>> GetItemsAsync([FromQuery] PaginationParams paginationParams)
    {
        var pagedResponse = await  itemService.GetItemsAsync(paginationParams);
        if (pagedResponse.TotalRecords == 0)
        {
            return NotFound();
        }
        return Ok(pagedResponse);
    }
}