using complete_app.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace complete_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    [EnableRateLimiting("Global")]
    public ActionResult<IEnumerable<Item>> GetItems()
    {
        var items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
            new Item { Id = 3, Name = "Item 3" },
        };
        return Ok(items);
    }
}
