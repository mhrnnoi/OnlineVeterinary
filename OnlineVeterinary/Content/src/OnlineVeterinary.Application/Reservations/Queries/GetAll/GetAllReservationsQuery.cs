using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.ReservedTimes.Queries.GetAll
{
    public record GetAllReservationsQuery() : IRequest<List<ReservationDTO>>;
    
}