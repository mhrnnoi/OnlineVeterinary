using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.Auth.Commands.ChangeEmail;
using OnlineVeterinary.Application.Auth.Commands.ChangePassword;
using OnlineVeterinary.Application.Auth.Commands.Delete;
using OnlineVeterinary.Application.Auth.Commands.Register;
using OnlineVeterinary.Application.Auth.Queries.Login;
using OnlineVeterinary.Contracts.Authentication.Request;

namespace OnlineVeterinary.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;
        public AuthenticationController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {

            var command = _mapper.Map<RegisterCommand>(request);

            var result = await _mediatR.Send(command);

            return result.Match(result => CreatedAtAction("Register", result), errors => Problem(errors));



        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {

            var command = _mapper.Map<LoginCommand>(request);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result), errors => Problem(errors));



        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyAccountAsync()
        {
            string userId = GetUserId();

            var command = new DeleteMyAccountCommand(userId);
            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result), errors => Problem(errors));




        }

      

        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            string userId = GetUserId();

            var command = new ChangePasswordCommand( Id : userId, NewPassword : request.NewPassword);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result), errors => Problem(errors));



        }
        [HttpPut]
        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailRequest request)
        {
            string userId = GetUserId();

            var command = new ChangeEmailCommand(request.NewEmail, userId);

            var result = await _mediatR.Send(command);

            return result.Match(result => Ok(result), errors => Problem(errors));



        }
        



    }
}