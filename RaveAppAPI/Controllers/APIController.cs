using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RaveAppAPI.Services.Helpers;
using System.Reflection;

namespace RaveAppAPI.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }
        if (errors.Any(e => e.Type == ErrorType.Unexpected))
        {
            try
            {
                dynamic obj = Problem().Value.GetType().GetProperties().ToList().Find(p => p.Name == "Extensions").GetValue(Problem().Value);
                string s = obj["traceId"];
                Logger.LogError(obj["traceId"]);
            }
            catch (Exception e)
            {
                Logger.LogError($"Error recuperando traceid de Problem(): {e.Message}");
            }
            return Problem();
        }

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}