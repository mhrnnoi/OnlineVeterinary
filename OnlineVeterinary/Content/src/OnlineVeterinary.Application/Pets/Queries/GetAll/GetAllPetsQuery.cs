using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Queries.GetAll
{
    public record GetAllPetsQuery : IRequest<List<PetDTO>>;
    
}