using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ToysController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public ToysController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost("owner/{selectedChildId:int}")]
    public async Task<ActionResult> CreateAsync([FromRoute] int selectedChildId,
        [FromBody] Toy toCreateToy)
    {
        try
        {
            await dataAccess.CreateToyAsync(selectedChildId, toCreateToy);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}