using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Api.Identity;
using OnlineVeterinary.Application.Features.Reservations.Commands.Add;
using OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById;
using OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll;
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
        [RequiresClaim(ClaimTypes.Role, "caregiver", "doctor")]

        public async Task<IActionResult> GetMyAllReservationsAsync()
        {
            var userId = GetUserId(User.Claims);
            var userRole = GetUserRole(User.Claims);
            var query = new GetAllReservationsQuery(userId, userRole);
            var result = await _mediatR.Send(query);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }




        [HttpPost]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> AddReservationAsync(AddReservationRequest request)
        {
            var userId = GetUserId(User.Claims);
            var command = new AddReservationCommand(request.PetId,
                                                     request.DoctorId, userId);

            var result = await _mediatR.Send(command);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }

        [HttpDelete]
        [RequiresClaim(ClaimTypes.Role, "caregiver", "doctor")]

        public async Task<IActionResult> DeleteMyReservationByIdAsync(Guid id)
        {
            var userId = GetUserId(User.Claims);
            var userRole = GetUserRole(User.Claims);
            var command = new DeleteReservationByIdCommand(id, userId, userRole);

            var result = await _mediatR.Send(command);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));
        }






    }
}