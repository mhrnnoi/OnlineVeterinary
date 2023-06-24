using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Queries.GetById
{
    public record GetReservationByIdQuery(Guid Id) : IRequest<ErrorOr<ReservationDTO>>;
    
}