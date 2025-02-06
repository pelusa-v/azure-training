using full_app_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace full_app_api.Controllers;


[Authorize(Policy = "ItemsRead")]
[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{

    private static List<Item> items = new()
    {
        new Item { Id = Guid.NewGuid(), Name = "Item 1", UserEmail = "Pink@jeanleonvoutlook.onmicrosoft.com" },
        new Item { Id = Guid.NewGuid(), Name = "Item 2", UserEmail = "Pink@jeanleonvoutlook.onmicrosoft.com" },
        new Item { Id = Guid.NewGuid(), Name = "Item 3", UserEmail = "OtherEmail@email.com" }
    };

    [HttpGet("")]
    public List<Item> GetItemsByEmail()
    {
        return items;
    }
}
