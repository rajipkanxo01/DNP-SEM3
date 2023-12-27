using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChildsController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public ChildsController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost]
    public async Task<ActionResult<Child>> CreateAsync(Child toCreateChild)
    {
        try
        {
            Child createdChild = await dataAccess.AddChildAsync(toCreateChild);
            return Created($"/childs/{createdChild.Id}", createdChild);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Child>>> GetAllChildsAsync([FromQuery] bool? favouriteFilerValue,
        [FromQuery] string? genderFilter)
    {
        try
        {
            ICollection<Child> allChilds = await dataAccess.GetAllChilds(favouriteFilerValue,genderFilter);
            return Ok(allChilds);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("toy/{toyId:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int toyId)
    {
        try
        {
            await dataAccess.RemoveToy(toyId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}