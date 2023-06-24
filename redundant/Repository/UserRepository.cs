// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using System.Threading;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using OnlineVeterinary.Controllers.Services;
// using OnlineVeterinary.Data;
// using OnlineVeterinary.Models;
// using OnlineVeterinary.Models.DTOs;
// using OnlineVeterinary.Models.Identity;

// namespace OnlineVeterinary.Repository
// {
//     public class UserRepository
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly DataContext _context;
//         private readonly RoleManager<IdentityRole> _roleManager;
//         private readonly IConfiguration _config;

//         public UserRepository(
//             UserManager<IdentityUser> userManager,
//             DataContext context, 
//             RoleManager<IdentityRole> roleManager,
//             IConfiguration config)
//         {
//             _userManager = userManager;
//             _context = context;
//             _roleManager = roleManager;
//             _config = config;
//         }
        
//         public async Task<> CreateUser([FromBody] UserRegisterationDTO userRegister)
//         {
            

//             var userSearchResult = await _userManager.FindByEmailAsync(userRegister.Email);

            
//             var identityUser = new IdentityUser()
//             {
//                 Email = userRegister.Email,
//                 UserName = userRegister.UserName


//             };

//             var createUserResult = await _userManagar.CreateAsync(identityUser, userRegister.Password);

//             if (createUserResult.Succeeded)
//             {
//                 await _userManagar.AddToRoleAsync(identityUser, userRegister.UserRole.ToString());
//                 await _context.SaveChangesAsync();
//                 await AddingToDataBaseAsync(userRegister);
//                 var token = await GenerateTokenAsync(identityUser);

//                 return Ok(AuthResponse.Success(token));

//             }

//             return BadRequest(AuthResponse.SomethingWentWrong());
//         }
//     }
// }