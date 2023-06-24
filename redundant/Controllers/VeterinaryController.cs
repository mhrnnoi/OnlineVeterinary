using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
using OnlineVeterinary.Models.DTOs.Incoming;
using OnlineVeterinary.Models.DTOs.OutGoing;
using OnlineVeterinary.Models.Identity;

namespace OnlineVeterinary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VeterinaryController : ControllerBase
    {
        private DataContext _context;
        private UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public VeterinaryController(DataContext context,
                 UserManager<IdentityUser> userManager,
                 IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCareGiversAsync()
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                return Ok(_mapper.Map<CareGiverDTO>(await _context.CareGivers.ToListAsync()));
            }
            return NotFound("There is no CareGiver");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            if (await _context.ReservedTimes.CountAsync() > 0)
            {
                return Ok(_mapper.Map<ReservedTimesDTO>(await _context.ReservedTimes.ToListAsync()));
            }

            return NotFound("There is no Reservation");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPetsAsync()
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                return Ok(_mapper.Map<PetDTO>(await _context.Pets.ToListAsync()));
            }
            return NotFound("There is no Pet");

        }
        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                return Ok(_mapper.Map<DoctorDTO>(await _context.Doctors.ToListAsync()));
            }

            return NotFound("There is no doctor");

        }



        [HttpGet]
        public async Task<IActionResult> GetSpecificDoctorAsync(string doctorUsername)
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Doctors.AnyAsync(a => a.UserName == doctorUsername)))
                {
                    return NotFound($"there is no  doctor with {doctorUsername} email");
                }
                var doctor = _mapper.Map<DoctorDTO>(await _context.Doctors.SingleAsync(a => a.UserName == doctorUsername));
                //return doctor DTO
                return Ok(doctor);
            }

            return NotFound("There is no doctor");

        }
        [HttpGet]
        public async Task<IActionResult> GetSpecigicCareGiverAsync(string careGiverUsername)
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.CareGivers.AnyAsync(a => a.Email == careGiverUsername)))
                {
                    return NotFound($"there is no caregiver with {careGiverUsername} email");
                }
                //return cg DTO
                var careGiver = _mapper.Map<CareGiverDTO>(await _context.CareGivers.SingleAsync(a => a.UserName == careGiverUsername));


                return Ok(careGiver);
            }

            return NotFound("There is no caregiver");

        }

        [HttpGet]
        public async Task<ActionResult> GetSpecificReservation(string code)
        {
            if (await _context.ReservedTimes.CountAsync() > 0)
            {
                var Guidcode = Guid.Parse(code);
                return Ok(_mapper.Map<ReservedTimesDTO>(await _context.ReservedTimes.SingleAsync(a => a.Code == Guidcode)));
            }

            return Ok("There is no doctor");

        }

        [HttpGet]
        public async Task<IActionResult> GetSpecificPetAsync(string petUsername)
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Pets.AnyAsync(a => a.Username == petUsername)))
                {
                    return NotFound($"there is no pet with {petUsername} username");
                }
                var pet = _mapper.Map<PetDTO>(await _context.Pets.SingleAsync(a => a.Username == petUsername));

                return Ok(pet);
            }

            return NotFound("There is no pet");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Doctor")]
        //doctors just can delete him or her self
        public async Task<IActionResult> DeleteSpecificDoctorAsync(string doctorUsernamer)
        {
            if (await _context.Doctors.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Doctors.AnyAsync(a => a.UserName == doctorUsernamer)))
                {
                    return BadRequest($"there is no doctor with {doctorUsernamer} username");
                }
                //check user role and email
                if (HttpContext.User.Claims.Any(a => a.Value == "Admin"))
                {
                    _context.Doctors.Remove(await _context.Doctors.SingleAsync(a => a.UserName == doctorUsernamer));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(doctorUsernamer));
                    await _context.SaveChangesAsync();
                    return Ok("deleted by Admin");
                }
                else if (HttpContext.User.Claims.Any(a => a.Value == doctorUsernamer))
                {
                    _context.Doctors.Remove(await _context.Doctors.SingleAsync(a => a.UserName == doctorUsernamer));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(doctorUsernamer));
                    await _context.SaveChangesAsync();
                    return Ok("deleted by you");
                }
                return BadRequest("you can't delete someone else plz login with the account you want delete ");

            }

            return NotFound("There is no doctor");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,CareGiver")]
        //cg can delete him or her self
        public async Task<IActionResult> DeleteSpecificCareGiverAsync(string careGiverUsername)
        {
            if (await _context.CareGivers.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.CareGivers.AnyAsync(a => a.UserName == careGiverUsername)))
                {
                    return BadRequest($"there is not careGiver with {careGiverUsername} email");
                }
                if (HttpContext.User.Claims.Any(a => a.Value == "Admin"))
                {
                    _context.CareGivers.Remove(await _context.CareGivers.SingleAsync(a => a.UserName == careGiverUsername));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(careGiverUsername));
                    await _context.SaveChangesAsync();
                    return Ok("deleted by Admin");
                }
                else if (HttpContext.User.Claims.Any(a => a.Value == careGiverUsername))
                {
                    _context.CareGivers.Remove(await _context.CareGivers.SingleAsync(a => a.UserName == careGiverUsername));
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(careGiverUsername));
                    await _context.SaveChangesAsync();
                    return Ok("deleted by You");
                }
                return BadRequest("you can't delete someone else plz login with the account you want delete ");

            }

            return Ok("There is no careGiver");

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,CareGiver")]

        public async Task<IActionResult> DeleteSpecificPetAsync(string petUsername)
        {
            if (await _context.Pets.CountAsync() > 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("what ?");
                }
                else if (!(await _context.Pets.AnyAsync(a => a.Username == petUsername)))
                {
                    return BadRequest($"there is no  pet with {petUsername} username");
                }
                var Targetpet = await _context.Pets.SingleAsync(a => a.Username == petUsername);

                if (HttpContext.User.Claims.Any(a => a.Value == "Admin"))
                {
                    _context.Pets.Remove(Targetpet);
                    await _context.SaveChangesAsync();
                    return Ok("deleted by Admin");
                }
                else
                {
                    var petCareGiverId = await _context.CareGiverPet.FirstOrDefaultAsync(a => a.PetId == Targetpet.Id);
                    var careGiverId = await _context.CareGivers.FirstAsync(a => a.Id == petCareGiverId.CareGiverId);
                    if (HttpContext.User.Claims.Any(a => a.Value == careGiverId.Email))
                    {
                        _context.Pets.Remove(await _context.Pets.SingleAsync(a => a.Username == petUsername));
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    return BadRequest("you can't delete someone else pet");
                }

            }

            return Ok("There is no pet");

        }













        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]
        public async Task<IActionResult> AddPetAsync([FromBody] PetRegisterDTO pet)
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
            //new Pet()
            // {
            //     PetType = pet.PetType,
            //     Sickness = pet.Sickness,
            //     Name = pet.Name,
            //     DateOfBirth = pet.DateOfBirth,
            //     Username = pet.Username

            // };
            var newPet = _mapper.Map<Pet>(pet);
            newPet.TimesOfCured = 0;

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

        [HttpGet]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]

        public async Task<IActionResult> GetAllPetsOfSpecificCareGiverAsync(string careGiverUsername)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("what do you entered");
            }
            if (!await _context.CareGivers.AnyAsync(a => careGiverUsername == a.UserName))
            {
                return NotFound("there is no cg with this user name");
            }
            var targetCareGiver = await _context.CareGivers.SingleOrDefaultAsync(a => a.UserName == careGiverUsername);

            if (!await _context.CareGiverPet.AnyAsync(a => a.CareGiverId == targetCareGiver.Id))
            {
                return NotFound("you dont have any pet");
            }
            // if (!HttpContext.User.Claims.Any(a => a.Value == careGiver.Email))
            //     {
            //         return BadRequest("you cant get some")
            //     }
            var targetCareGiverPet = await _context.CareGiverPet.Where(a => a.CareGiverId == targetCareGiver.Id).ToListAsync();
            var pets = new List<Pet>();
            foreach (var row in targetCareGiverPet)
            {
                pets.Add(await _context.Pets.FirstAsync(a => a.Id == row.PetId));
            }

            return Ok(_mapper.Map<PetDTO>(pets));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]
        public async Task<IActionResult> FormaVisitAsSoonAsPossibleAsync([FromQuery] string doctorUsername, [FromBody] string petUserName)
        {
            var WorkTimeStart = Convert.ToDateTime("07:0:0");
            var WorkTimeEnd = Convert.ToDateTime("21:0:0");

            if (!ModelState.IsValid)
            {
                return BadRequest("what is that ");
            }

            var IsDrExist = await _context.Doctors.AnyAsync(a => a.UserName == doctorUsername);
            if (!IsDrExist)
            {
                return BadRequest("dr is not exist");
            }
            var IsPetExist = await _context.Pets.AnyAsync(a => a.Username == petUserName);
            if (!IsPetExist)
            {
                return BadRequest("pet is not exist");
            }
            var careGiverEmail = HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Email).Value;

            var targetCareGiver = await _context.CareGivers.SingleOrDefaultAsync(a => a.Email == careGiverEmail);

            if (!await _context.CareGiverPet.AnyAsync(a => a.CareGiverId == targetCareGiver.Id))
            {
                return BadRequest("you dont have any pet");
            }
            var targetPet = await _context.Pets.SingleOrDefaultAsync(a => a.Username == petUserName);
            var careGiverPetrow = _context.CareGiverPet.Where(a => a.Id == targetCareGiver.Id);

            if (!await careGiverPetrow.AnyAsync(a => a.PetId == targetPet.Id))
            {
                return BadRequest("you dont have this pet");

            }

            var targetDr = await _context.Doctors.SingleOrDefaultAsync(a => a.UserName == doctorUsername);


            var drReservedTimes = await _context.ReservedTimes.Where(a => a.DrId == targetDr.Id).CountAsync();
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
                        PetId = targetPet.Id,
                        CareGiverUserName = targetCareGiver.UserName,
                        DrUserName = targetDr.UserName,
                        PetUserName = petUserName,
                        Code = Guid.NewGuid(),
                        



                    });
                    await _context.SaveChangesAsync();
                    return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));
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
                        PetId = targetPet.Id,
                        CareGiverUserName = targetCareGiver.UserName,
                        DrUserName = targetDr.UserName,
                        PetUserName = petUserName,
                        Code = Guid.NewGuid()



                    });
                    await _context.SaveChangesAsync();

                    return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));

                }

            }
            else
            {
                var lastReserved = await _context.ReservedTimes.Where(a => a.DrId == targetDr.Id).OrderBy(a => a.ReservedTime).LastOrDefaultAsync();

                if (lastReserved.ReservedTime.AddMinutes(30).TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay)
                {
                    var reservedTime = lastReserved.ReservedTime.AddMinutes(30);
                    await _context.ReservedTimes.AddAsync(new ReservedTimes()

                    {
                        ReservedTime = reservedTime,
                        DrId = targetDr.Id,
                        CgId = targetCareGiver.Id,
                        PetId = targetPet.Id,

                        CareGiverUserName = targetCareGiver.UserName,
                        DrUserName = targetDr.UserName,
                        PetUserName = petUserName,
                        Code = Guid.NewGuid()


                    });
                    await _context.SaveChangesAsync();
                    return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));
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
                        PetId = targetPet.Id,

                        CareGiverUserName = targetCareGiver.UserName,
                        DrUserName = targetDr.UserName,
                        PetUserName = petUserName,
                        Code = Guid.NewGuid()


                    });
                    await _context.SaveChangesAsync();
                    return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));

                }
            }


        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "CareGiver")]

        public async Task<IActionResult> FormaVisitSpecificTimeAsync([FromQuery] string doctorUsername, [FromBody] string petUserName, DateTime time)
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
            if (!(time.TimeOfDay >= WorkTimeStart.TimeOfDay && time.TimeOfDay <= WorkTimeEnd.AddMinutes(-30).TimeOfDay))
            {
                return BadRequest("the time you entered is out of work time ");
            }


            var IsDrExist = await _context.Doctors.AnyAsync(a => a.UserName == doctorUsername);
            if (!IsDrExist)
            {
                return BadRequest("dr is not exist");
            }


            var IsPetExist = await _context.Pets.AnyAsync(a => a.Username == petUserName);
            if (!IsPetExist)
            {
                return BadRequest("pet is not exist");
            }
            var careGiverEmail = HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Email).Value;

            var targetCareGiver = await _context.CareGivers.SingleOrDefaultAsync(a => a.Email == careGiverEmail);

            if (!await _context.CareGiverPet.AnyAsync(a => a.CareGiverId == targetCareGiver.Id))
            {
                return BadRequest("you dont have any pet");
            }
            var targetPet = await _context.Pets.SingleOrDefaultAsync(a => a.Username == petUserName);
            var careGiverPetrow = _context.CareGiverPet.Where(a => a.Id == targetCareGiver.Id);

            if (!await careGiverPetrow.AnyAsync(a => a.PetId == targetPet.Id))
            {
                return BadRequest("you dont have this pet");

            }

            var targetDr = await _context.Doctors.SingleOrDefaultAsync(a => a.UserName == doctorUsername);

            var drReservedTimes = await _context.ReservedTimes.Where(a => a.DrId == targetDr.Id).CountAsync();


            if (drReservedTimes < 1)
            {

                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes()

                {
                    ReservedTime = reservedTime,
                    DrId = targetDr.Id,
                    CgId = targetCareGiver.Id,
                    PetId = targetPet.Id,

                    CareGiverUserName = targetCareGiver.UserName,
                    DrUserName = targetDr.UserName,
                    PetUserName = petUserName,
                        Code = Guid.NewGuid()



                });
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));

            }
            var checkIsExist = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime == time);
            if (checkIsExist)
            {
                return BadRequest("the time you want is booked by another person");
            }
            var checkDay = await _context.ReservedTimes.AnyAsync(a => a.ReservedTime.Day == time.Day);
            if (!checkDay)
            {
                var reservedTime = time;
                await _context.ReservedTimes.AddAsync(new ReservedTimes()

                {
                    ReservedTime = reservedTime,
                    DrId = targetDr.Id,
                    CgId = targetCareGiver.Id,
                    PetId = targetPet.Id,

                    CareGiverUserName = targetCareGiver.UserName,
                    DrUserName = targetDr.UserName,
                    PetUserName = petUserName,
                        Code = Guid.NewGuid()


                });
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));


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
                            DrId = targetDr.Id,
                            CgId = targetCareGiver.Id,
                            PetId = targetPet.Id,

                            CareGiverUserName = targetCareGiver.UserName,
                            DrUserName = targetDr.UserName,
                            PetUserName = petUserName,
                        Code = Guid.NewGuid()


                        });
                        await _context.SaveChangesAsync();
                        return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));

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
                            DrId = targetDr.Id,
                            CgId = targetCareGiver.Id,
                            PetId = targetPet.Id,

                            CareGiverUserName = targetCareGiver.UserName,
                            DrUserName = targetDr.UserName,
                            PetUserName = petUserName,
                        Code = Guid.NewGuid()


                        });
                        await _context.SaveChangesAsync();
                        return Ok(reservedTime);

                    }
                    else
                    {
                        return BadRequest("your time has problem  (not enough time for visit cuz of another person near to this time(bigger)) plz pick another time");
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
                            DrId = targetDr.Id,
                            CgId = targetCareGiver.Id,
                            PetId = targetPet.Id,

                            CareGiverUserName = targetCareGiver.UserName,
                            DrUserName = targetDr.UserName,
                            PetUserName = petUserName,
                        Code = Guid.NewGuid()


                        });
                        await _context.SaveChangesAsync();
                        return Ok(_mapper.Map<ReservedTimesDTO>(reservedTime));
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