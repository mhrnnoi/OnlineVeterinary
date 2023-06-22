using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVeterinary.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            var firstError = errors.First();
            var myStatusCode =  firstError.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unexpected => StatusCodes.Status406NotAcceptable,
                ErrorType.Failure => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode : myStatusCode, title : firstError.Description);
        }
       
    }
}