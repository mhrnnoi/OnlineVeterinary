using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineVeterinary.Data;
using OnlineVeterinary.Models;
using OnlineVeterinary.Models.DTOs;

namespace OnlineVeterinary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private UserManager<IdentityUser> _userManagar;
        private DataContext Context { get; }

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration config, DataContext context)
        {
            Context = context;
            _config = config;
            _userManagar = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDTO userRegister)
        {
            if (!(ModelState.IsValid))
            {
                return BadRequest(new AuthResponse()
                {
                    Error = new List<string> { "entered invalid" },
                    Result = false,
                    Token = null

                });
            }
            var existedUser = await _userManagar.FindByEmailAsync(userRegister.Email);
            if (existedUser != null)
            {
                return BadRequest(new AuthResponse()
                {
                    Error = new List<string> { "email is already signed up" },
                    Result = false,
                    Token = null

                });
            }
            var newUser = new IdentityUser()
            {
                Email = userRegister.Email,
                UserName = userRegister.UserName,

            };
            var creatingUser = await _userManagar.CreateAsync(newUser, userRegister.Password);
            if (creatingUser.Succeeded)
            {
                var token = GenerateToken(userRegister);

                if (userRegister.IsDr)
                {
                    Context.Doctors.Add(new Doctor() 
                    {
                        UserName = userRegister.UserName
                    });
                }
                if (!userRegister.IsDr)
                {
                    Context.CareGivers.Add(new CareGiver() 
                    {
                        UserName = userRegister.UserName
                    });
                }
                await Context.SaveChangesAsync();

                return Ok(new AuthResponse()
                {
                    Error = null,
                    Result = true,
                    Token = token

                });

            }
            return BadRequest(new AuthResponse()
            {
                Error = new List<string> { "something went wrong" },
                Result = false,
                Token = null

            });

        }

        private string GenerateToken(IUserDto user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtConfig:Secret"]);

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Email),
                    new Claim("IsDr", user.IsDr.ToString())

                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost]

        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse()
                {
                    Error = new List<string> { "entered invalid input" },
                    Result = false,
                    Token = null

                });
            }
            var existedUser = await _userManagar.FindByEmailAsync(userLogin.Email);
            if (existedUser == null)
            {
                return BadRequest(new AuthResponse()
                {
                    Error = new List<string> { "email is not signed up" },
                    Result = false,
                    Token = null

                });
            }


            var isCorrectPass = await _userManagar.CheckPasswordAsync(existedUser, userLogin.Password);
            if (isCorrectPass)
            {
                var token = GenerateToken(userLogin);
                return Ok(new AuthResponse()
                {
                    Error = null,
                    Result = true,
                    Token = token

                });

            }
            return BadRequest(new AuthResponse()
            {
                Error = new List<string> { "incorrect password" },
                Result = false,
                Token = null

            });

        }


    }
}