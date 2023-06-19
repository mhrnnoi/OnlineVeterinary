using System;

namespace OnlineVeterinary.Contracts.Doctors.Request
{
    public record AddDoctorRequest(string FirstName, string LastName, string Email, string Password);
   
}
