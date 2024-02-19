using Demo.Api.Data;
using Demo.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private AppDbContext _appDbContext;
        public SongsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return _appDbContext.Songs;
            return  Ok(await _appDbContext.Songs.ToListAsync());
            // return BadRequest(); // 400 
            // return NotFound();   // 404
            //return StatusCode(StatusCodes.Status200OK);

        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _appDbContext.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");

            return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            await _appDbContext.Songs.AddAsync(song);
            await _appDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songRequest)
        {
            var song = await _appDbContext.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");
            
            song.Title = songRequest.Title;
            await _appDbContext.SaveChangesAsync();
            
            
            return Ok("record updated succesfully");
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _appDbContext.Songs.FindAsync(id);
            if (song == null)
                return NotFound($"No record found with the id {id}");

            _appDbContext.Remove(song);
            await _appDbContext.SaveChangesAsync();
            return Ok("record deleted");
        }
    }
}
