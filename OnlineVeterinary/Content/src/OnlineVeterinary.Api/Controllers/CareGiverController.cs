using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVeterinary.Application.CareGivers.Queries.GetAll;
using OnlineVeterinary.Application.CareGivers.Queries.GetById;

namespace OnlineVeterinary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CareGiverController : ApiController
    {
        private readonly IMediator _mediatR;
        public CareGiverController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCareGiversAsync()
        {
            var query = new GetAllCareGiversQuery();
            var careGivers = await _mediatR.Send(query);
            return Ok(careGivers);

        }
        [HttpGet]
        public async Task<IActionResult> GetCareGiverByIdAsync(Guid id)
        {
            var query = new GetCareGiverByIdQuery(id);
            var careGiver = await _mediatR.Send(query);
            return Ok(careGiver);

        }
    }
}