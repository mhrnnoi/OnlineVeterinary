using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVeterinary.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected string GetUserId(IEnumerable<Claim> claims)
        {
            //i set user id to sub 
            
            return claims.Single(a=> a.Type.ToLower() == "id").Value;
        }
        protected string GetUserFamilyName(IEnumerable<Claim> claims)
        {
            return  claims.First(a => a.Type == JwtRegisteredClaimNames.FamilyName).Value;
        }
        protected string GetUserRole(IEnumerable<Claim> claims)
        {
            return claims.First(a => a.Type == ClaimTypes.Role).Value;
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