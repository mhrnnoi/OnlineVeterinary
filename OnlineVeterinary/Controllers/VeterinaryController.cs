using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public VeterinaryController(DataContext context,
                 UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCareGiversAsync()
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                return Ok(_context.CareGivers.ToList());
            }
            return NotFound("There is no CareGiver");

        }
        [HttpGet]
        public async Task<IActionResult> GetReservationsAsync()
        {
            if (await _context.ReservedTimes.CountAsync() > 0)
            {
                return Ok(_context.ReservedTimes.ToList());
            }

            return NotFound("There is no Reservation");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPetsAsync()
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                return Ok(_context.Pets.ToList());
            }
            return NotFound("There is no Pet");

        }
        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                return Ok(_context.Doctors.ToList());
            }

            return NotFound("There is no doctor");

        }



        [HttpPost]
        public async Task<IActionResult> GetSpecificDoctorAsync([FromBody] DoctorDTO doctor)
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Doctors.AnyAsync(a => a.Email == doctor.Email)))
                {
                    return NotFound($"there is no  doctor with {doctor.Email} email");
                }
                return Ok(await _context.Doctors.SingleAsync(a => a.Email == doctor.Email));
            }

            return NotFound("There is no doctor");

        }
        [HttpPost]
        public async Task<IActionResult> GetSpecigicCareGiverAsync([FromBody] CareGiverDTO careGiver)
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.CareGivers.AnyAsync(a => a.Email == careGiver.Email)))
                {
                    return NotFound($"there is no caregiver with {careGiver.Email} email");
                }
                return Ok(await _context.CareGivers.SingleAsync(a => a.Email == careGiver.Email));
            }

            return NotFound("There is no caregiver");

        }
        [HttpPost]
        public async Task<IActionResult> GetSpecificPetAsync([FromBody] PetDTO pet)
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Pets.AnyAsync(a => a.Username == pet.Username)))
                {
                    return NotFound($"there is no pet with {pet.Username} username");
                }
                return Ok(await _context.Pets.SingleAsync(a => a.Username == pet.Username));
            }

            return NotFound("There is no pet");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Doctor")]
        public async Task<IActionResult> DeleteSpecificDoctorAsync([FromBody] DoctorDTO doctor)
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Doctors.AnyAsync(a => a.Email == doctor.Email)))
                {
                    return BadRequest($"there is no doctor with {doctor.Email} email");
                }

                if (HttpContext.User.Claims.Any(a => a.Value == doctor.Email))
                {
                    _context.Doctors.Remove(await _context.Doctors.SingleAsync(a => a.Email == doctor.Email));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(doctor.Email));
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("you can't delete someone else plz login with the account you want delete ");

            }

            return NotFound("There is no doctor");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,CareGiver")]

        public async Task<IActionResult> DeleteSpecificCareGiverAsync([FromBody] CareGiverDTO careGiver)
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.CareGivers.AnyAsync(a => a.Email == careGiver.Email)))
                {
                    return BadRequest($"there is not careGiver with {careGiver.Email} email");
                }
                if (HttpContext.User.Claims.Any(a => a.Value == careGiver.Email))
                {
                    _context.CareGivers.Remove(await _context.CareGivers.SingleAsync(a => a.Email == careGiver.Email));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(careGiver.Email));
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("you can't delete someone else plz login with the account you want delete ");

            }

            return Ok("There is no careGiver");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,CareGiver")]

        public async Task<IActionResult> DeleteSpecificPetAsync([FromBody] PetDTO pet)
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Pets.AnyAsync(a => a.Username == pet.Username)))
                {
                    return BadRequest($"there is no  pet with {pet.Username} username");
                }
                var Targetpet = await _context.Pets.SingleAsync(a => a.Username == pet.Username);
                var petCareGiverId = await _context.CareGiverPet.FirstOrDefaultAsync(a => a.PetId == Targetpet.Id);
                var careGiverId = await _context.CareGivers.FirstAsync(a => a.Id == petCareGiverId.CareGiverId);
                if (HttpContext.User.Claims.Any(a => a.Value == careGiverId.Email))
                {
                    _context.Pets.Remove(await _context.Pets.SingleAsync(a => a.Username == pet.Username));
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("you can't delete someone else pet");
            }

            return Ok("There is no pet");

        }



        // [HttpGet]
        // public async Task<ActionResult> GetAllVisitorsAsync()
        // {
        //     if (await _context.Visitors.CountAsync() > 0)
        //     {
        //         return Ok(_context.Visitors.ToList());
        //     }

        //     return Ok("There is no Visitor");

        // }
        // [HttpPost]
        // public async Task<ActionResult> GetVistor([FromBody] Visitor doctor)
        // {
        //     if (_context.Doctors.Count() > 0)
        //     {
        //         return Ok(await _context.Doctors.SingleAsync(a=> a.UserName == doctor.UserName));
        //     }

        //     return Ok("There is no doctor");

        // }

        // [HttpPost]
        // public async Task<ActionResult> GetSpecificReservation([FromBody] DoctorDTO doctor)
        // {
        //     if (await _context.Doctors.CountAsync() > 0)
        //     {
        //         return Ok(await _context.Doctors.SingleAsync(a=> a.UserName == doctor.UserName));
        //     }

        //     return Ok("There is no doctor");

        // }







        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]
        public async Task<IActionResult> AddPetAsync([FromBody] PetDTO pet)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }




            var careGiverEmail = HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Email).Value;

            var careGiver = await _context.CareGivers.FirstAsync(a => a.Email == careGiverEmail);
            var careGiverPets = _context.CareGiverPet.Where(a => a.CareGiverId == careGiver.Id);
            var pets = new List<Pet>();
            foreach (var item in _context.Pets)
            {
                var exist = await careGiverPets.AnyAsync(a => a.PetId == item.Id);
                if (exist && item.Username == pet.Username)
                {
                    return BadRequest("the pet whith this user name is already added");
                }

            }

            var newPet = new Pet()
            {
                PetType = pet.PetType,
                Sickness = pet.Sickness,
                Name = pet.Name,
                DateOfBirth = pet.DateOfBirth
            };
            await _context.Pets.AddAsync(newPet);
            await _context.SaveChangesAsync();
            await _context.CareGiverPet.AddAsync(new CareGiverPet()
            {
                // PetId = _context.Pets.First(a => a.Name == newPet.Name).Id,
                PetId = newPet.Id,
                CareGiverId = careGiver.Id

            });
            await _context.SaveChangesAsync();






            return Ok("Pet Added");
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]

        public async Task<IActionResult> GetAllPetsOfSpecificCareGiverAsync([FromBody] CareGiverDTO careGiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("what do you entered");
            }
            if (!await _context.CareGivers.AnyAsync(a => careGiver.Email == a.Email))
            {
                return NotFound("you dont have any pet");
            }
            // if (!HttpContext.User.Claims.Any(a => a.Value == careGiver.Email))
            //     {
            //         return BadRequest("you cant get some")
            //     }
            var targetCareGiver = await _context.CareGivers.FirstAsync(a => a.Email == careGiver.Email);
            var targetCareGiverPet = _context.CareGiverPet.Where(a => a.CareGiverId == targetCareGiver.Id).ToList();
            var pets = new List<Pet>();
            foreach (var row in targetCareGiverPet)
            {
                pets.Add(await _context.Pets.FirstAsync(a => a.Id == row.PetId));
            }

            return Ok(pets);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]
        public async Task<IActionResult> FormaVisitAsSoonAsPossibleAsync([FromBody] DoctorDTO doctor)
        {
            var WorkTimeStart = Convert.ToDateTime("07:0:0");
            var WorkTimeEnd = Convert.ToDateTime("21:0:0");

            if (!ModelState.IsValid)
            {
                return BadRequest("what is that ");
            }

            var IsDrExist = await _context.Doctors.AnyAsync(a => a.UserName == doctor.Email);
            if (!IsDrExist)
            {
                return BadRequest("dr is not exist");
            }

            var targetDr = await _context.Doctors.SingleOrDefaultAsync(a => a.Email == doctor.Email);
            var careGiverEmail = HttpContext.User.Claims.First(a => a.Type == JwtRegisteredClaimNames.Email).Value;
            var targetCareGiver = await _context.CareGivers.SingleOrDefaultAsync(a => a.Email == careGiverEmail);
            if (!await _context.CareGiverPet.AnyAsync(a => a.CareGiverId == targetCareGiver.Id))
            {
                return BadRequest("you dont have any pet");
            }
            var careGiverPetrow = await _context.CareGiverPet.FirstOrDefaultAsync(a => a.Id == targetCareGiver.Id);
            var drReservedTimes = await _context.ReservedTimes.Where(a => a.DrId == targetCareGiver.Id).CountAsync();
            if (drReservedTimes < 1)
            {
                if (DateTime.Now.AddHours(1).TimeOfDay >= WorkTimeStart.TimeOfDay && DateTime.Now.AddHours(1).TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay)
                {
                    var reservedTime = DateTime.Now.AddHours(1);
                    await _context.ReservedTimes.AddAsync(new ReservedTimes()

                    {
                        ReservedTime = reservedTime,
                        DrId = targetDr.Id,
                        CgId = targetCareGiver.Id,
                        PetId = careGiverPetrow.PetId

                    });
                    await _context.SaveChangesAsync();
                    return Ok(reservedTime);
                }
                else
                {
                    var tomarow = DateTime.Now.AddDays(1);

                    var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
                    await _context.ReservedTimes.AddAsync(new ReservedTimes()

                    {
                        ReservedTime = reservedTime,
                        DrId = targetDr.Id,
                        CgId = targetCareGiver.Id,
                        PetId = careGiverPetrow.PetId

                    });
                    await _context.SaveChangesAsync();

                    return Ok(reservedTime);

                }

            }
            else
            {
                var lastReserved = _context.ReservedTimes.Where(a => a.DrId == targetDr.Id).OrderBy(a => a.ReservedTime).Last();

                if (lastReserved.ReservedTime.AddMinutes(30).TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay)
                {
                    var reservedTime = lastReserved.ReservedTime.AddMinutes(30);
                    await _context.ReservedTimes.AddAsync(new ReservedTimes()

                    {
                        ReservedTime = reservedTime,
                        DrId = targetDr.Id,
                        CgId = targetCareGiver.Id,
                        PetId = careGiverPetrow.PetId

                    });
                    await _context.SaveChangesAsync();
                    return Ok(reservedTime);
                }
                else
                {

                    var LastTime = lastReserved.ReservedTime;
                    var tomarow = LastTime.AddDays(1);
                    var reservedTime = new DateTime(tomarow.Year, tomarow.Month, tomarow.Day, 07, 0, 0);
                    await _context.ReservedTimes.AddAsync(new ReservedTimes()

                    {
                        ReservedTime = reservedTime,
                        DrId = targetDr.Id,
                        CgId = targetCareGiver.Id,
                        PetId = careGiverPetrow.PetId

                    });
                    await _context.SaveChangesAsync();
                    return Ok(reservedTime);

                }
            }


        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]

        public async Task<IActionResult> FormaVisitSpecificTimeAsync([FromQuery] DoctorDTO doctor, [FromBody] DateTime time)
        {
            var WorkTimeStart = Convert.ToDateTime("07:0:0");
            var WorkTimeEnd = Convert.ToDateTime("21:0:0");

            if (!ModelState.IsValid)
            {
                return BadRequest("you entered invalid");
            }

            if (time < DateTime.Now)
            {
                return BadRequest("the time you entered is past time");

            }


            var isDrExist = await _context.Doctors.AnyAsync(a => a.Email == doctor.Email);

            if (!isDrExist)
            {
                return BadRequest("dr is not exist");
            }

            if (!(time.TimeOfDay >= WorkTimeStart.TimeOfDay && time.TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay))
            {
                return BadRequest("the time you entered is out of work time ");
            }
            var dr = await _context.Doctors.SingleOrDefaultAsync(a => a.Email == doctor.Email);
            var drReservedTimes = await _context.ReservedTimes.Where(a => a.DrId == dr.Id).CountAsync();

            var cgEmail = HttpContext.User.Claims.First(a => a.Type == JwtRegisteredClaimNames.Email).Value;
            var cg = await _context.CareGivers.SingleOrDefaultAsync(a => a.Email == cgEmail);
            if (!await _context.CareGiverPet.AnyAsync(a => a.CareGiverId == cg.Id))
            {
                return BadRequest("you dont have any pet");
            }
            var petrow = await _context.CareGiverPet.FirstOrDefaultAsync(a => a.Id == cg.Id);


            if (drReservedTimes < 1)
            {

                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes()

                {
                    ReservedTime = reservedTime,
                    DrId = dr.Id,
                    CgId = cg.Id,
                    PetId = petrow.PetId

                });
                await _context.SaveChangesAsync();
                return Ok(reservedTime);

            }
            var checkIsExist = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime == time);
            if (checkIsExist)
            {
                return BadRequest("the you want book is booked by another person");
            }
            var checkDay = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime.Day == time.Day);
            if (!checkDay)
            {
                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes()

                {
                    ReservedTime = reservedTime,
                    DrId = dr.Id,
                    CgId = cg.Id,
                    PetId = petrow.PetId

                });
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
                        await _context.ReservedTimes.AddAsync(new ReservedTimes()

                        {
                            ReservedTime = reservedTime,
                            DrId = dr.Id,
                            CgId = cg.Id,
                            PetId = petrow.PetId

                        });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);

                    }
                    else
                    {
                        return BadRequest("your time is between 2 seasion plz pick another time");
                    }
                }
                else if (isThereBigger)
                {
                    var closestBig = await _context.ReservedTimes.Where(a => a.ReservedTime > time).MinAsync(a => a.ReservedTime);

                    if (closestBig.AddMinutes(-30) >= time)
                    {
                        var reservedTime = time;
                        await _context.ReservedTimes.AddAsync(new ReservedTimes()

                        {
                            ReservedTime = reservedTime,
                            DrId = dr.Id,
                            CgId = cg.Id,
                            PetId = petrow.PetId

                        });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);

                    }
                    else
                    {
                        return BadRequest("you time has conflic with its close bigger time plz pick another time");
                    }
                }
                else
                {
                    var closestSmall = await _context.ReservedTimes.Where(a => a.ReservedTime < time).MaxAsync(a => a.ReservedTime);

                    if (closestSmall.AddMinutes(30) <= time)
                    {
                        var reservedTime = time;
                        await _context.ReservedTimes.AddAsync(new ReservedTimes()

                        {
                            ReservedTime = reservedTime,
                            DrId = dr.Id,
                            CgId = cg.Id,
                            PetId = petrow.PetId

                        });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);
                    }
                    else
                    {
                        return BadRequest("your time is in another seasion plz pick another time ");
                    }
                }




            }



        }
    }
}