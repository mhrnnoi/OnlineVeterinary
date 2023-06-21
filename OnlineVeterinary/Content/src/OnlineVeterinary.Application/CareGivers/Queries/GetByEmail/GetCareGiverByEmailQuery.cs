using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetByEmail
{
    public record GetCareGiverByEmailQuery(string email) : IRequest<ErrorOr<CareGiverDTO>>;
   
}