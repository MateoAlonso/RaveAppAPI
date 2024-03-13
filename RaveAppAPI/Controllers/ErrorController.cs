using Microsoft.AspNetCore.Mvc;
namespace RaveAppAPI.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    protected IActionResult Error()
    {
        return Problem();
    }
}