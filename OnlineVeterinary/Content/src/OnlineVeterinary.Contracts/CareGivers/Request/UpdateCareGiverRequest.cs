using System;

namespace OnlineVeterinary.Contracts.CareGivers.Request
{
    public record UpdateCareGiverRequest(Guid Id, string FirstName, string LastName, string Email, string Password);

}
