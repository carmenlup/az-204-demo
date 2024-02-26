using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Api.Data;
using Demo.Api.Models;
using Azure.Storage.Blobs;
using Demo.Api.Services;
using Demo.Api.Data.Entities;
using System.Drawing.Printing;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IFileService _processFileService;
        public ArtistsController(AppDbContext context, IFileService processFileService)
        {
            _context = context;
            _processFileService = processFileService;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistModel>>> GetArtist(int? pageNumber = 1, int? pageSize = 5)
        {

            var artists = await _context.Artists.Select( a => 
            new ArtistModel
            {
                Id = a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl
            }).ToListAsync();
            
            return Ok(artists.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize));
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artists = await _context.Artists
                .Where(a => a.Id == id)
                .Include(s => s.Songs)
                .ToListAsync();

            if (artists == null)
            {
                return NotFound();
            }

            var artistDetail =  artists.Find(a => a.Id == id);
            return Ok(artistDetail);
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistModel artist)
        {
            
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Not used in FE in phase one
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist([FromForm] Artist artist)
        {
            var imageUrl = await _processFileService.UploadFile(artist.Image);
            artist.ImageUrl = imageUrl;

            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            // return Results.Ok()
            return StatusCode(StatusCodes.Status201Created);
            //_context.Atrists.Add(artist);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }



        // DELETE: api/Artists/5
        // Not used in FE in phase one
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
