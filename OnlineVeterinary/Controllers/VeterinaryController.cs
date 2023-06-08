using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Data;
using OnlineVeterinary.Models;
using OnlineVeterinary.Models.DTOs;
using OnlineVeterinary.Models.Identity;

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

            return Ok("There is no doctor");

        }


        [HttpGet]
        public ActionResult GetAllVisitors()
        {
            if (_context.Visitors.Count() > 0)
            {
                return Ok(_context.Visitors.ToList());
            }

            return Ok("There is no Visitor");

        }
        [HttpGet]
        public ActionResult GetAllPets()
        {
            if (_context.Pets.Count() > 0)
            {
                return Ok(_context.Pets.ToList());
            }
            return Ok("There is no Pets");

        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]
        public async Task<IActionResult> AddPetAsync([FromBody] PetDTO pet)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("InvalidInput");
            }



            var careGiverEmail = HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Email).Value;

            var careGiver = _context.CareGivers.First(a => a.Email == careGiverEmail);
            var newPet = new Pet()
            {
                PetType = pet.PetType,
                Sickness = pet.Sickness,
                Name = pet.Name,
                DateOfBirth = pet.DateOfBirth
            };
            _context.Pets.Add(newPet);
            await _context.SaveChangesAsync();
            _context.CareGiverPet.Add(new CareGiverPet()
            {
                PetId = _context.Pets.First(a => a.Name == newPet.Name).Id,
                CareGiverId = careGiver.Id

            });
            await _context.SaveChangesAsync();






            return Ok("Pet Added");
        }

        [HttpPost]
        public IActionResult GetAllPets([FromBody] CareGiverDTO careGiver)
        {
            var targetId = _context.CareGivers.First(a => a.Email == careGiver.Email).Id;
            var targetRows = _context.CareGiverPet.Where(a => a.CareGiverId == targetId).ToList();
            var pets = new List<Pet>();
            foreach (var row in targetRows)
            {
                pets.Add(_context.Pets.First(a => a.Id == row.PetId));
            }

            return Ok(pets);
        }

        [HttpPost]
        public IActionResult FormaVisitAsSoonAsPossible([FromBody] DoctorDTO doctor)
        {
            var WorkTimeStart = Convert.ToDateTime("07:0:0");
            var WorkTimeEnd = Convert.ToDateTime("21:0:0");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var CheckDrExist = _context.Doctors.First(a => a.UserName == doctor.UserName);
            if (CheckDrExist == null)
            {
                return BadRequest();
            }

            if (_context.ReservedTimes.Count() < 1)
            {
                if (DateTime.Now.AddHours(1).TimeOfDay >= WorkTimeStart.TimeOfDay && DateTime.Now.AddHours(1).TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay)
                {
                    var reservedTime = DateTime.Now.AddHours(1);
                    _context.ReservedTimes.Add(new ReservedTimes() { ReservedTime = reservedTime });
                    _context.SaveChanges();
                    return Ok(reservedTime);
                }
                else
                {
                    var tomarow = DateTime.Now.AddDays(1);

                    var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
                    _context.ReservedTimes.Add(new ReservedTimes() { ReservedTime = reservedTime });
                    _context.SaveChanges();

                    return Ok(reservedTime);

                }

            }
            else
            {
                var lastReserved = _context.ReservedTimes.OrderBy(a => a.ReservedTime).Last();
                if (lastReserved.ReservedTime.AddMinutes(30).TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay)
                {
                    var reservedTime = lastReserved.ReservedTime.AddMinutes(30);
                    _context.ReservedTimes.Add(new ReservedTimes() { ReservedTime = reservedTime });
                    _context.SaveChanges();
                    return Ok(reservedTime);
                }
                else
                {

                    var LastTime = lastReserved.ReservedTime;
                    var tomarow = LastTime.AddDays(1);
                    var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
                    _context.ReservedTimes.Add(new ReservedTimes() { ReservedTime = reservedTime });
                    _context.SaveChanges();
                    return Ok(reservedTime);

                }
            }


        }
        [HttpPost]
        public async Task<IActionResult> FormaVisitSpecificTimeAsync([FromQuery] DoctorDTO doctor, [FromBody] DateTime time)
        {
            var WorkTimeStart = Convert.ToDateTime("07:0:0");
            var WorkTimeEnd = Convert.ToDateTime("21:0:0");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (time < DateTime.Now)
            {
                return BadRequest();

            }


            var CheckDrExist = await _context.Doctors.FirstAsync(a => a.UserName == doctor.UserName);

            if (CheckDrExist == null)
            {
                return BadRequest();
            }

            if (!(time.TimeOfDay >= WorkTimeStart.TimeOfDay && time.TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay))
            {
                return BadRequest();
            }
            var count = await _context.ReservedTimes.CountAsync();
            if (count < 1)
            {

                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes() { ReservedTime = reservedTime });
                await _context.SaveChangesAsync();
                return Ok(reservedTime);

            }
            var checkIsExist = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime == time);
            if (checkIsExist)
            {
                return BadRequest();
            }
            var checkDay = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime.Day == time.Day);
            if (!checkDay)
            {
                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes() { ReservedTime = reservedTime });
                await _context.SaveChangesAsync();
                return Ok(reservedTime);


            }

            else
            {
                var isThereSmaller = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime < time);
                var isThereBigger = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime > time);



                if (isThereBigger && isThereSmaller)
                {
                    var closestSmall = await _context.ReservedTimes.Where(a => a.ReservedTime < time).MaxAsync(a => a.ReservedTime);
                    var closestBig = await _context.ReservedTimes.Where(a => a.ReservedTime > time).MinAsync(a => a.ReservedTime);
                    if (closestSmall.AddMinutes(30) <= time && closestBig.AddMinutes(-30) >= time)
                    {
                        var reservedTime = time;
                        await _context.ReservedTimes.AddAsync(new ReservedTimes() { ReservedTime = reservedTime });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else if (isThereBigger)
                {
                    var closestBig = await _context.ReservedTimes.Where(a => a.ReservedTime > time).MinAsync(a => a.ReservedTime);

                    if (closestBig.AddMinutes(-30) >= time)
                    {
                        var reservedTime = time;
                        await _context.ReservedTimes.AddAsync(new ReservedTimes() { ReservedTime = reservedTime });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    var closestSmall = await _context.ReservedTimes.Where(a => a.ReservedTime < time).MaxAsync(a => a.ReservedTime);

                    if (closestSmall.AddMinutes(30) <= time)
                    {
                        var reservedTime = time;
                        await _context.ReservedTimes.AddAsync(new ReservedTimes() { ReservedTime = reservedTime });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }




            }



        }
    }
}