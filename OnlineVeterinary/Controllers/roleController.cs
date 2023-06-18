using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineVeterinary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private RoleManager<IdentityRole> _rolemanager;

        public RoleController(RoleManager<IdentityRole> rolemanager)
        {
            _rolemanager = rolemanager;
        }
        [HttpPost]
                // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                //              Roles = "Admin")]

        public async Task<IActionResult> CreateRoleAsync( RoleEnum role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter valid role ");
            }
            var CheckRoleExist = await _rolemanager.FindByNameAsync(role.ToString()) != null;
            if (CheckRoleExist)
            {
                return BadRequest("This role already exist");
            }
            await _rolemanager.CreateAsync(new IdentityRole(role.ToString()));
            return Ok(_rolemanager.Roles.ToListAsync());
        }

        [HttpDelete]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<IActionResult> DeleteRoleAsync( RoleEnum role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter valid role ");
            }
            var CheckRoleExist = await _rolemanager.FindByNameAsync(role.ToString()) ;
            if (CheckRoleExist == null)
            {
                return BadRequest("This role is not there ");
            }
            await _rolemanager.DeleteAsync(CheckRoleExist);
            return Ok(_rolemanager.Roles.ToListAsync());
        }
    }
}