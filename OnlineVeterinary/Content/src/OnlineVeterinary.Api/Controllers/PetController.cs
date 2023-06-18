using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVeterinary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public PetController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }
        GetAllPetsAsync
        GetSpecificPetAsync
        DeleteSpecificPetAsync
        AddPetAsync

        [HttpPost]
        public async Task<IActionResult> AddCareGiverAsync(AddCareGiverRequest request)
        {
            var command = _mapper.Map<AddCareGiverCommand>(request);

            var careGiver = await _mediatR.Send(command);
            return Ok(careGiver);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllCareGiversAsync()
        {
            var query = new GetAllCareGiversQuery();
            var careGivers = await _mediatR.Send(query);
            return Ok(careGivers);

        }
        [HttpGet]
        public async Task<IActionResult> GetCareGiverByIdAsync(Guid id)
        {
            var query = new GetCareGiverByIdQuery(id);
            var careGiver = await _mediatR.Send(query);
            return Ok(careGiver);

        }

        [HttpGet]
        public async Task<IActionResult> GetPetsOfCareGiverByIdAsync(Guid id)
        {
            var query = new GetPetsOfCareGiverByIdQuery(id);
            var pets = await _mediatR.Send(query);
            return Ok(pets);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCareGiverAsync(UpdateCareGiverRequest request)
        {
            var command = _mapper.Map<UpdateCareGiverCommand>(request);

            var careGiver = await _mediatR.Send(command);
            return Ok(careGiver);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCareGiverByIdAsync(Guid id)
        {
            var query = new DeleteCareGiverByIdCommand(id);
            var result = await _mediatR.Send(query);
            return Ok(result);

        }
    }
}