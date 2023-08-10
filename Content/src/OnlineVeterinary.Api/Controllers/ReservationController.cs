using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineVeterinary.Api.Filters;
using OnlineVeterinary.Api.Identity;
using OnlineVeterinary.Application.Features.Reservations.Commands.Add;
using OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById;
using OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll;
using OnlineVeterinary.Contracts.Reservations.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(ReservationFilterAttribute))]
    public class ReservationController : ApiController
    {
        private readonly IStringLocalizer<ReservationController> _localizer;

        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public ReservationController(ISender sender, IMapper mapper, IStringLocalizer<ReservationController> localizer)
        {
            _sender = sender;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver", "doctor")]

        public async Task<IActionResult> GetMyAllReservationsAsync(CancellationToken cancellationToken)
        {
            var userId = GetUserId(User.Claims);
            var userRole = GetUserRole(User.Claims);
            var query = new GetAllReservationsQuery(userId, userRole);
            var result = await _sender.Send(query, cancellationToken);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }




        [HttpPost]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> AddReservationAsync([FromBody] AddReservationRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserId(User.Claims);
            var command = new AddReservationCommand(request.PetId,
                                                     request.DoctorId, userId);

            var result = await _sender.Send(command, cancellationToken);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }

        [HttpDelete("{id}")]
        [RequiresClaim(ClaimTypes.Role, "caregiver", "doctor")]

        public async Task<IActionResult> DeleteMyReservationByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId(User.Claims);
            var userRole = GetUserRole(User.Claims);
            var command = new DeleteReservationByIdCommand(id, userId, userRole);

            var result = await _sender.Send(command, cancellationToken);
            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));
        }






    }
}