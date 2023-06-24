using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Api.Identity;
using OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetById;
using OnlineVeterinary.Application.Features.Pets.Commands.Add;
using OnlineVeterinary.Application.Features.Pets.Commands.Delete;
using OnlineVeterinary.Contracts.Pets.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnimalCareController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public AnimalCareController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpPost]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]
        public async Task<IActionResult> AddPetAsync(AddPetRequest request)
        {
            var petInfo = _mapper.Map<AddPetCommand>((request));
            var userId = GetUserId(User.Claims);
            var userFamilyName = GetUserFamilyName(User.Claims);
            var command = petInfo with
            {
                CareGiverId = Guid.Parse(userId),
                CareGiverLastName = userFamilyName
            };

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }



        [HttpDelete]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> DeleteMyPetByIdAsync(Guid id)
        {
            var userId = GetUserId(User.Claims);
            var command = new DeletePetByIdCommand(id, userId);
            var result = await _mediatR.Send(command);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }


        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetDoctorByIdAsync(Guid id)
        {
            var query = new GetDoctorByIdQuery(id);
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }

        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetMyPetsAsync()
        {
            var userId = GetUserId(User.Claims);
            var query = new GetMyPetsQuery(userId);
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }


        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetAllDoctorsAsync()
        {
            var query = new GetAllDoctorsQuery();
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }




    }
}