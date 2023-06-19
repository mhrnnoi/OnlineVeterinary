using System;

namespace OnlineVeterinary.Contracts.Doctors.Request
{
    public record UpdateDoctorRequest(Guid Id, string FirstName, string LastName, string Email, string Password);

}
