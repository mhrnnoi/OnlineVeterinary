using System.IdentityModel.Tokens.Jwt;
using ErrorOr;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVeterinary.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected string GetUserId()
        {
            //i set user id to sub 
            return User.Claims.Single(a=> a.Type.ToLower() == "id").Value;
        }
        protected string GetUserName()
        {
            return  User.Claims.First(a => a.Type == JwtRegisteredClaimNames.FamilyName).Value;
        }

        [AllowAnonymous]
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