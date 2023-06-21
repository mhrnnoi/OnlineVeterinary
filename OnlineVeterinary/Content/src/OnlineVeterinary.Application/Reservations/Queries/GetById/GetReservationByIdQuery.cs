using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Queries.GetById
{
    public record GetReservationByIdQuery(Guid Id) : IRequest<ReservationDTO>;
    
}