using System;

namespace OnlineVeterinary.Contracts.CareGivers.Request
{
    public record AddCareGiverRequest(string FirstName, string LastName, string Email, string Password);
    
}
