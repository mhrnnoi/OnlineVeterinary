using System;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Application.DTOs;
using OnlineVeterinary.Domain.Role.Enums;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Users.Commands.Add
{
    public record AddUserCommand(string FirstName, string LastName, string Email, string Password, int Role) : IRequest<ErrorOr<User>>;
    
}
