using Demo.Api.Data;
using Demo.Api.Data.Entities;
using Demo.Api.Models;
using Demo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IFileService _processFileService;
        public AlbumsController(AppDbContext context, IFileService processFileService)
        {
            _context = context;
            _processFileService = processFileService;
        }

        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum([FromForm] Album album)
        {
            var imageUrl = await _processFileService.UploadFile(album.Image);

            album.ImageUrl = imageUrl;

            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetArtist()
        {

            var album = await _context.Albums.Select(a =>
            new AlbumModel
            {
                Id = a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl
            }).ToListAsync();

            return Ok(album);
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var albums = await _context.Albums
                .Where(a => a.Id == id)
                .Include(s => s.Songs)
                .ToListAsync();

            if (albums == null)
            {
                return NotFound();
            }

            var albumDetail = albums.Find(a => a.Id == id);
            return Ok(albumDetail);
        }

        // PUT api/Albums/5
        // Not used in FE in phase one
        [HttpPut("{id}")]
        public async Task<ActionResult<Album>> Put(int id, [FromBody] Album albumRequest)
        {
            var album = await _context.Albums.FindAsync(id);

            if (album == null)
                return NotFound($"No Album found for the id {id}");

            album.Name = albumRequest.Name;
            album.ArtistId = albumRequest.ArtistId;
            await _context.SaveChangesAsync();


            return Ok("record updated succesfully");
        }

    }
}
