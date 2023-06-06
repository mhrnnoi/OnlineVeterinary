using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Data;
using OnlineVeterinary.Models;

namespace OnlineVeterinary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VeterinaryController : ControllerBase
    {
        private DataContext _context;
        private UserManager<IdentityUser> _userManager;

        public VeterinaryController(DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult GetAllDoctors()
        {
            if (_context.Doctors.Count() > 0)
            {
                return Ok(_context.Doctors.ToList());
            }
            else
            {
                return Ok("There is no doctor");
            }
        }

        [HttpGet]
        public ActionResult GetAllVisitors()
        {
            if (_context.Visitors.Count() > 0)
            {
                return Ok(_context.Visitors.ToList());
            }
            else
            {
                return Ok("There is no Visitor");
            }
        }
        [HttpGet]
        public ActionResult GetAllPets()
        {
            if (_context.Pets.Count() > 0)
            {
                return Ok(_context.Pets.ToList());
            }
            else
            {
                return Ok("There is no Pets");
            }
        }
        

        [HttpPost("adopterName/petName/DateOfBirth/PetType")]
        public IActionResult AddPet(string adopterName, string petName, DateTime dateOfBirth, PetEnum petType)
        {
            if (_context.CareGivers.Count() < 1)
            {
                return NotFound("there is no adopter ");
                
            }

            else if (_context.CareGivers.First(a => a.FullName == adopterName) == null)
            {
                return NotFound("there is no adopter with that name");
            }
            
            var targetAdopter = _context.CareGivers.First(a => a.FullName == adopterName);
            var newPet = new Pet()
            {
                Name = petName,
                DateOfBirth = dateOfBirth,
                CareGiver = targetAdopter,
                PetType = petType

            };
            _context.Pets.Add(newPet);

            targetAdopter.Pets.Add(newPet);
        
            return CreatedAtAction("AddPet",newPet);
        }
    }
}