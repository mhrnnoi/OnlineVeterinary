using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet3back.dbcontext;
using aspnet3back.models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet3back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly Datacontext _context;

        public GamesController(Datacontext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Games>> Get()
        {
            return _context.Games.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetbyId(int id)
        {
            if (await _context.Games.FindAsync(id) == null)
            {
                return NotFound();
            }
            return _context.Games.Find(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Games game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { id = game.Id }, game);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Games game)
        {

            var game1 = await _context.Games.FindAsync(id);
            game1.Name = game.Name;
            game1.Platform = game.Platform;


            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game1 = await _context.Games.FindAsync(id);

            _context.Games.Remove(game1);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}