using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineVeterinary.Api.Identity;
using OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetById;
using OnlineVeterinary.Application.Features.Pets.Commands.Add;
using OnlineVeterinary.Application.Features.Pets.Commands.Delete;
using OnlineVeterinary.Contracts.Pets.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnimalCareController : ApiController
    {
        private readonly IStringLocalizer<AnimalCareController> _localizer;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public AnimalCareController(ISender sender, IMapper mapper, IStringLocalizer<AnimalCareController> localizer)
        {
            _sender = sender;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpPost]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]
        public async Task<IActionResult> AddPetAsync(AddPetRequest request, CancellationToken cancellationToken)
        {
            var petInfo = _mapper.Map<AddPetCommand>((request));
            var userId = GetUserId(User.Claims);
            var userFamilyName = GetUserFamilyName(User.Claims);
            var command = petInfo with
            {
                CareGiverId = Guid.Parse(userId),
                CareGiverLastName = userFamilyName
            };

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));

        }



        [HttpDelete("{id}")]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> DeleteMyPetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId(User.Claims);
            var command = new DeletePetByIdCommand(id, userId);
            var result = await _sender.Send(command, cancellationToken);
            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));

        }


        [HttpGet("{id}")]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDoctorByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }

        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetMyPetsAsync(CancellationToken cancellationToken)
        {
            var userId = GetUserId(User.Claims);
            var query = new GetMyPetsQuery(userId);
            var result = await _sender.Send(query, cancellationToken);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }


        [HttpGet]
        [RequiresClaim(ClaimTypes.Role, "caregiver")]

        public async Task<IActionResult> GetAllDoctorsAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllDoctorsQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.Match(result => Ok(result),
                                 errors => Problem(errors));

        }




    }
}