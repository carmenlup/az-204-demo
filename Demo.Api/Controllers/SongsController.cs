using Demo.Api.Data;
using Demo.Api.Data.Entities;
using Demo.Api.Models;
using Demo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        // POST api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> Post([FromForm] Song song)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var imageUrl = await _processFileService.UploadFile(song.Image);
#pragma warning restore CS8604 // Possible null reference argument.
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

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<SongModel>> Get(int? pageNumber = 1, int? pageSize = 5)
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

            return Ok(songs.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize));
            // return  Ok(await _appDbContext.Songs.ToListAsync());
            // return BadRequest(); // 400 
            // return NotFound();   // 404
            //return StatusCode(StatusCodes.Status200OK);

        }

        // GET api/Songs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");

            return Ok(song);
        }

        

        // GET: api/Songs/FeaturedSongs/?isFeatured=true
        [HttpGet("[action]")]
        public async Task<ActionResult<SongModel>> FeaturedSongs(bool isFeatured)
        {
            var songs = await _context.Songs.Where(s=> s.IsFeatured == isFeatured).Select(s =>
            new SongModel
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                ImageUrl = s.ImageUrl,
                AudioUrl = s.AudioUrl
            }).ToListAsync();

            return Ok(songs);
        }

        // GET: api/Songs/NewSongs
        [HttpGet("[action]")]
        public async Task<ActionResult<SongModel>> NewSongs()
        {
            var songs = await _context.Songs.Select(s =>
            new SongModel
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                ImageUrl = s.ImageUrl,
                AudioUrl = s.AudioUrl,
                Date = s.UploadTime
            }).OrderByDescending(s=> s.Date).ToListAsync();

            return Ok(songs);
        }

        // GET: api/Songs/SearchSongs/?query=a
        [HttpGet("[action]")]
        public async Task<ActionResult<SongModel>> SearchSongs(string query)
        {
            var songs = await _context.Songs.Where(s => s.Title.ToUpper().StartsWith(query.ToUpper())).Select(s =>
            new SongModel
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                ImageUrl = s.ImageUrl,
                AudioUrl = s.AudioUrl,
                Date = s.UploadTime
            }).ToListAsync();

            return Ok(songs);
        }

        // PUT api/Songs/5
        // Not used in FE in phase one
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songRequest)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                return NotFound($"No record found with the id {id}");
            
            song.AlbumId = songRequest.AlbumId;
            song.Title = songRequest.Title;
            song.IsFeatured = songRequest.IsFeatured;
            await _context.SaveChangesAsync();
            
            
            return Ok("record updated succesfully");
        }

        // DELETE api/Songs/5
        // Not used in FE in phase one
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
