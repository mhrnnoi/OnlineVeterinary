using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineVeterinary.Api.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string[] _claimValue;
        public RequiresClaimAttribute(string claimName , params string[] claimValues)
        {
            _claimValue = claimValues;
            _claimName = claimName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var value =  context.HttpContext.User.Claims.First(a=> a.Type == _claimName).Value;
            if (!_claimValue.Contains(value))
            {
                context.Result = new ForbidResult();

            }
        }
    }
}