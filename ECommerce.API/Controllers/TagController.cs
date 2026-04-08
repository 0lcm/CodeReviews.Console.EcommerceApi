using ECommerce.API.Interfaces;
using ECommerce.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostTag([FromBody] CreateTagDto tagDto)
    {
        await tagService.PostTagAsync(tagDto);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTag([FromQuery] int tagId)
    {
        var success = await tagService.DeleteTagByIdAsync(tagId);

        return success switch
        {
            null => NotFound(),
            true => NoContent(),
            false => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}