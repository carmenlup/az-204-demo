using Demo.Api.Data;
using Demo.Api.Data.Entities;
using Demo.Api.Models;
using Demo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private AppDbContext _context;
        private IFileService _processFileService;
        public SongsController(AppDbContext context, IFileService processFileService)
        {
            _context = context;
            _processFileService = processFileService;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<ActionResult<SongModel>> Get()
        {
            var songs = await _context.Songs.Select(s =>
            new SongModel
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                ImageUrl = s.ImageUrl,
                AudioUrl = s.AudioUrl
            }).ToListAsync();

            return Ok(songs);
            // return  Ok(await _appDbContext.Songs.ToListAsync());
            // return BadRequest(); // 400 
            // return NotFound();   // 404
            //return StatusCode(StatusCodes.Status200OK);

        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");

            return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<ActionResult<Song>> Post([FromForm] Song song)
        {
            var imageUrl = await _processFileService.UploadFile(song.Image);
            song.ImageUrl = imageUrl;
            if (song.AudioFile != null)
            {
                var audioFileUrl = await _processFileService.UploadFile(song.AudioFile);
                song.AudioUrl = audioFileUrl;
            }
            song.UploadTime = DateTime.Now;

            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songRequest)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");
            
            song.AlbumId = songRequest.AlbumId;
            song.Title = songRequest.Title;
            await _context.SaveChangesAsync();
            
            
            return Ok("record updated succesfully");
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
                return NotFound($"No record found with the id {id}");

            _context.Remove(song);
            await _context.SaveChangesAsync();
            return Ok("record deleted");
        }
    }
}
