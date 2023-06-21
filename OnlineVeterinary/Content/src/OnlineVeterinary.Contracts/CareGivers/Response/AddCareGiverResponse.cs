using System;

namespace OnlineVeterinary.Contracts.CareGivers.Response
{
    public record AddCareGiverResponse(Guid Id, string FirstName, string LastName, string Email, string Password);
    
}
