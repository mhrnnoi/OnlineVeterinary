// using Microsoft.AspNetCore.Mvc;

// namespace OnlineVeterinary.Api.Controllers
// {
//     [Route("api/[controller]")]
//     public class AdminController : ApiController
//     {
        
//                 // [HttpDelete]
//         // public async Task<IActionResult> DeleteCareGiverByIdAsync(Guid id)
//         // {
//         //     var command = new DeleteCareGiverByIdCommand(id);
//         //     var result = await _mediatR.Send(command);
//         //     return Ok(result);

//         // }

//         [HttpGet]
//         public async Task<IActionResult> GetPetsOfCareGiverByIdAsync(Guid id)
//         {
//             var query = new GetPetsOfCareGiverByIdQuery(id);
//             var petsDto = await _mediatR.Send(query);
//             return Ok(petsDto);

//         }
//               // }
//         // [HttpGet]
//         // public async Task<IActionResult> GetAllCareGiversAsync()
//         // {
//         //     var query = new GetAllCareGiversQuery();
//         //     var careGiverDto = await _mediatR.Send(query);
//         //     return Ok(careGiverDto);

//         // }

        
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetCareGiverByIdAsync(Guid id)
//         {
//             var query = new GetCareGiverByIdQuery(id);
//             var careGiverDto = await _mediatR.Send(query);
//             return Ok(careGiverDto);

//         }  
//     }
// }