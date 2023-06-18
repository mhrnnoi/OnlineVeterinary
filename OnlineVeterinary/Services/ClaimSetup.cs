using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineVeterinary.Models;

namespace OnlineVeterinary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ClaimSetup : Attribute, IAuthorizationFilter
    {
        private  string _claimvalue;
        public string Claimname ;
        public ClaimSetup(string claimname, string claimvalue)
        {
            Claimname = claimname;
            _claimvalue = claimvalue;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.HttpContext.User.HasClaim(Claimname, _claimvalue)))
            {
                context.Result =  new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
            }
        }
    }
}