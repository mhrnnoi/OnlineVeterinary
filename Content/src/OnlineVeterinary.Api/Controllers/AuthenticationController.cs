using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineVeterinary.Api.Filters;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword;
using OnlineVeterinary.Application.Features.Auth.Commands.Delete;
using OnlineVeterinary.Application.Features.Auth.Commands.Register;
using OnlineVeterinary.Application.Features.Auth.Queries.Login;
using OnlineVeterinary.Contracts.Authentication.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiController
    {
        private readonly IStringLocalizer<AuthenticationController> _localizer;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public AuthenticationController(IMediator mediatR, IMapper mapper, IStringLocalizer<AuthenticationController> localizer)
        {
            _mediatR = mediatR;
            _mapper = mapper;
            _localizer = localizer;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {

            var command = _mapper.Map<RegisterCommand>(request);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result),
                                 errors => Problem(errors));



        }
        [AllowAnonymous]
        [HttpPost]
        [ServiceFilter(typeof(LoginActionFilterAttribute))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {

            var command = _mapper.Map<LoginCommand>(request);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result),
                                 errors => Problem(errors));



        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyAccountAsync()
        {
            string userId = GetUserId(User.Claims);

            var command = new DeleteMyAccountCommand(userId);
            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));




        }

      

        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            string userId = GetUserId(User.Claims);

            var command = new ChangePasswordCommand( Id : userId,
                                                     NewPassword : request.NewPassword);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));



        }
        [HttpPut]
        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailRequest request)
        {
            string userId = GetUserId(User.Claims);

            var command = new ChangeEmailCommand(request.NewEmail,
                                                 userId);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(_localizer[result]),
                                 errors => Problem(errors));



        }
        



    }
}