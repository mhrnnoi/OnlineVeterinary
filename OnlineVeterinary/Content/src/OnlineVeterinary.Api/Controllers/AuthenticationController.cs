using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.Auth.Delete;
using OnlineVeterinary.Application.Auth.Login;
using OnlineVeterinary.Application.Auth.Register;
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
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            
            var command = _mapper.Map<RegisterCommand>(request);
            
            var result = await _mediatR.Send(command);
            
            return result.Match(result => CreatedAtAction("Register", result),   errors => Problem(errors));



        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginOrDeleteRequest request)
        {

            var command = _mapper.Map<LoginCommand>(request);
            
            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result),   errors => Problem(errors));



        }
       
        [HttpDelete]
        public async Task<IActionResult> DeleteAccountAsync([FromBody] LoginOrDeleteRequest request)
        {

            var command = _mapper.Map<DeleteCommand>(request);

            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result),   errors => Problem(errors));



        }
        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] LoginOrDeleteRequest request)
        {

            var command = _mapper.Map<DeleteCommand>(request);

            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result),   errors => Problem(errors));



        }
        [HttpPut]
        public async Task<IActionResult> ChangeEmailAsync([FromBody] LoginOrDeleteRequest request)
        {

            var command = _mapper.Map<DeleteCommand>(request);

            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result),   errors => Problem(errors));



        }
        [HttpPut]
        public async Task<IActionResult> LogOutAsync([FromBody] LoginOrDeleteRequest request)
        {

            var command = _mapper.Map<DeleteCommand>(request);

            var result = await _mediatR.Send(command);
            
            return result.Match(result => Ok(result),   errors => Problem(errors));



        }
      
        
        
    }
}