using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.Common;

namespace OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll
{
    public record GetAllReservationsQuery(string Id, string Role) : IRequest<ErrorOr<List<ReservationDTO>>>;
    
}