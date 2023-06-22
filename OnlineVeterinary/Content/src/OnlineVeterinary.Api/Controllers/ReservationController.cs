using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.Reservations.Commands.Add;
using OnlineVeterinary.Application.Reservations.Commands.AddCustom;
using OnlineVeterinary.Application.Reservations.Commands.DeleteById;
using OnlineVeterinary.Application.Reservations.Commands.Update;
using OnlineVeterinary.Application.Reservations.Queries.GetById;
using OnlineVeterinary.Application.ReservedTimes.Queries.GetAll;
using OnlineVeterinary.Contracts.Reservations.Entities;
using OnlineVeterinary.Contracts.Reservations.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReservationController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public ReservationController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var query = new GetAllReservationsQuery();
            var reservaionsDto = await _mediatR.Send(query);
            return Ok(reservaionsDto);

        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> GetReservationByIdAsync(Guid id)
        {
            var clm = new Claim("registeredjwt","role");
            HttpContext.User.HasClaim("slo","admin");
            var query = new GetReservationByIdQuery(id);
            var reservaionDto = await _mediatR.Send(query);
            return Ok(reservaionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservationAsync(AddReservationRequest request)
        {
            var command = _mapper.Map<AddReservationCommand>(request);

            var reservaionDto = await _mediatR.Send(command);
            return Ok(reservaionDto);
        }
        [HttpPost]

        public async Task<IActionResult> AddReservationCustomAsync(AddReservationCustomRequest request)
        {
            var command = _mapper.Map<AddReservationCustomCommand>(request);

            var reservaionDto = await _mediatR.Send(command);
            return Ok(reservaionDto);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReservationByIdAsync(Guid id)
        {
            var command = new DeleteReservationByIdCommand(id);

            var result = await _mediatR.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReservationAsync(UpdateReservationRequest request)
        {
            
            var command = _mapper.Map<UpdateReservationCommand>(request);

            var reservaionDto = await _mediatR.Send(command);
            return Ok(reservaionDto);
            
        }





    }
}