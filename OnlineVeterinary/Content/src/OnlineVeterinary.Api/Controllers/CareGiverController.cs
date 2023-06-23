using System.IdentityModel.Tokens.Jwt;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.CareGivers.Queries.GetPets;
using OnlineVeterinary.Application.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Doctors.Queries.GetById;
using OnlineVeterinary.Application.Pets.Commands.Add;
using OnlineVeterinary.Application.Pets.Commands.Delete;
using OnlineVeterinary.Contracts.Pets.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CareGiverController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public CareGiverController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPetAsync(AddPetRequest request)
        {
            var petInfo = _mapper.Map<AddPetCommand>((request));
            var userId = GetUserId();
            var userName = GetUserName();
            var command = petInfo with { CareGiverId = userId, CareGiverName = userName };

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result), errors => Problem(errors));

        }



        [HttpDelete]
        public async Task<IActionResult> DeleteMyPetByIdAsync(Guid id)
        {
            var userId = GetUserId();
            var command = new DeletePetByIdCommand(id, userId);
            var result = await _mediatR.Send(command);
            return result.Match(result => Ok(result), errors => Problem(errors));

        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid id)
        {
            var query = new GetDoctorByIdQuery(id);
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result), errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetMyPetsAsync()
        {
            var userId = GetUserId();
            var query = new GetPetsOfCareGiverByIdQuery(userId);
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result), errors => Problem(errors));

        }
    

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsAsync()
        {
            var query = new GetAllDoctorsQuery();
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result), errors => Problem(errors));

        }









    }
}