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

    [HttpDelete]
    public async Task<IActionResult> DeleteItemAsync([FromQuery] int itemId)
    {
        var success = await itemService.DeleteItemByIdAsync(itemId);
        if (success is null)
            return NotFound();
        if (success is false)
            return StatusCode(StatusCodes.Status500InternalServerError);
        
        return NoContent();
    }
}