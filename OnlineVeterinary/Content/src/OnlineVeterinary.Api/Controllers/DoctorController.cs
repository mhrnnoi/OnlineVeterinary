using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.Doctors.Commands.Add;
using OnlineVeterinary.Application.Doctors.Commands.DeleteById;
using OnlineVeterinary.Application.Doctors.Commands.Update;
using OnlineVeterinary.Application.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Doctors.Queries.GetById;
using OnlineVeterinary.Contracts.Doctors.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DoctorController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public DoctorController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(AddDoctorRequest request)
        {
            var command = _mapper.Map<AddDoctorCommand>(request);

            var doctorDto = await _mediatR.Send(command);
            return Ok(doctorDto);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsAsync()
        {
            var query = new GetAllDoctorsQuery();
            var doctorDto = await _mediatR.Send(query);
            return Ok(doctorDto);

        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid id)
        {
            var query = new GetDoctorByIdQuery(id);
            var doctorDto = await _mediatR.Send(query);
            return Ok(doctorDto);

        }

        // [HttpGet]
        // public async Task<IActionResult> GetPetsOfdoctorByIdAsync(Guid id)
        // {
        //     var query = new GetPetsOfdoctorByIdQuery(id);
        //     var petsDto = await _mediatR.Send(query);
        //     return Ok(petsDto);

        // }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            var command = _mapper.Map<UpdateDoctorCommand>(request);

            var doctorDto = await _mediatR.Send(command);
            return Ok(doctorDto);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorByIdAsync(Guid id)
        {
            var command = new DeleteDoctorByIdCommand(id);
            var result = await _mediatR.Send(command);
            return Ok(result);

        }


    }
}