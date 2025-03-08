using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Data;
using SuperHeroApp.Entities;

namespace SuperHeroApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero is null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            _context.SaveChanges();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updateHero)
        {
            
            var herodb = await _context.SuperHeroes.FindAsync(updateHero.Id);
            if (herodb is null)
            {
                return BadRequest("Hero not found");
            }
            herodb.Name = updateHero.Name;
            herodb.FirstName = updateHero.FirstName;
            herodb.LastName = updateHero.LastName;
            herodb.Place = updateHero.Place;

            _context.SaveChanges();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {

            var herodb = await _context.SuperHeroes.FindAsync(id);
            if (herodb is null)
            {
                return BadRequest("Hero not found");
            }

            _context.SuperHeroes.Remove(herodb);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
