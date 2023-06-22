using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.Pets.Commands.Add;
using OnlineVeterinary.Application.Pets.Commands.Delete;
using OnlineVeterinary.Application.Pets.Commands.Update;
using OnlineVeterinary.Application.Pets.Queries.GetAll;
using OnlineVeterinary.Application.Pets.Queries.GetCareGiver;
using OnlineVeterinary.Application.Pets.Queries.GetPet;
using OnlineVeterinary.Contracts.Pets.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PetController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public PetController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPetAsync(AddPetRequest request)
        {
            SignOut();
            var command = _mapper.Map<AddPetCommand>(request);

            var petDto = await _mediatR.Send(command);
            return Ok(petDto);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPetsAsync()
        {
            var query = new GetAllPetsQuery();
            var PetsDto = await _mediatR.Send(query);
            return Ok(PetsDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetPetByIdAsync(Guid id)
        {
            var query = new GetPetByIdQuery(id);
            var PetsDto = await _mediatR.Send(query);
            return Ok(PetsDto);

        }

        [HttpGet]
        public async Task<IActionResult> GetCareGiverOfPetByIdAsync(Guid id)
        {
            var query = new GetCareGiverOfPetByIdQuery(id);
            var careGiverDto = await _mediatR.Send(query);
            return Ok(careGiverDto);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePetAsync(UpdatePetRequest request)
        {
            var command = _mapper.Map<UpdatePetCommand>(request);

            var petDto = await _mediatR.Send(command);
            return Ok(petDto);

        }

        [HttpDelete]
        public async Task<IActionResult> DeletePetByIdAsync(Guid id)
        {
            var command = new DeletePetByIdCommand(id);
            var result = await _mediatR.Send(command);
            return Ok(result);

        }
    }

}