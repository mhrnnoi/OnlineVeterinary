using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetById
{
    public record GetCareGiverByIdQuery(Guid Id) : IRequest<ErrorOr<CareGiverDTO>>;
    
}