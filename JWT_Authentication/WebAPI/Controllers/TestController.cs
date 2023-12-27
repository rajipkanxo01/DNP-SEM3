using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet("authorized")]
    public ActionResult GetAsAuthorized()
    {
        return Ok("This was accepted as authorized");
    }

    [HttpGet("allowanyone"), AllowAnonymous]
    public ActionResult GetAsAnyone()
    {
        return Ok("This was accepted as anonymous");
    }

    [HttpGet("mustbevia"), Authorize("MustBeVia")]
    public ActionResult GetAsVia()
    {
        return Ok("This was accepted as via domain");
    }

    [HttpGet("manualcheck")]
    public ActionResult GetWithManualCheck()
    {
        Claim? claim = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role));

        if (claim == null)
        {
            return StatusCode(403, "You have no role");
        }

        return claim.Value.Equals("Teacher") switch
        {
            false => StatusCode(403, "You are not a teacher"),
            _ => Ok("You are a teacher, you may proceed")
        };
    }
}