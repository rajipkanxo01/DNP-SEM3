using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public AlbumsController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbumAsync(Album toCreateAlbum)
    {
        try
        {
            try
            {
                Album createdAlbum = await dataAccess.AddAlbumAsync(toCreateAlbum);
                return Created($"/albums/{createdAlbum.Title}", createdAlbum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("titles")]
    public async Task<ActionResult<ICollection<string>>> GetAlbumTitlesAsync()
    {
        try
        {
            ICollection<string> allTitles = await dataAccess.GetAlbumTitlesAsync();
            return new ActionResult<ICollection<string>>(allTitles);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost, Route("{albumTitle}")]
    public async Task<ActionResult<Image>> AddImageToAlbumAsync([FromBody] Image img, [FromRoute] string albumTitle)
    {
        try
        {
            Image image = await dataAccess.AddImageToAlbumAsync(albumTitle, img);
            return new ActionResult<Image>(image);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}