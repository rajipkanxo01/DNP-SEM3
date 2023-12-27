using System.Net.Mime;
using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public ImagesController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Image>>> GetAllAuthorsAsync([FromQuery] string? albumCreator,
        [FromQuery] string? albumTopic)
    {
        try
        {
            ICollection<Image> allImages = await dataAccess.GetImagesAsync(albumCreator, albumTopic);
            return new ActionResult<ICollection<Image>>(allImages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}