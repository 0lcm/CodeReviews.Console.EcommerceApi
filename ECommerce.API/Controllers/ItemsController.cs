using ECommerce.API.Interfaces;
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
}