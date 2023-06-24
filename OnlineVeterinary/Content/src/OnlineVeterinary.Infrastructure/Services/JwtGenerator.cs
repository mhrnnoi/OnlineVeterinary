using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineVeterinary.Application.Common;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Infrastructure.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtSettings _jwtOptions;
        private IDateTimeProvider _dateTimeProvider;

        public JwtGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
        {
            _jwtOptions = jwtOptions.Value;
            _dateTimeProvider = dateTimeProvider;
        }
        public Jwt GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            var mySigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha512);
            var myCliaims = new [] 
            {
                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("id",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            var securityToken =  new JwtSecurityToken(claims : myCliaims, audience: _jwtOptions.Audience, signingCredentials : mySigningCredentials, expires : _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes));
            var handler =  new  JwtSecurityTokenHandler();
            return new Jwt(handler.WriteToken(securityToken));
        }
    }
}