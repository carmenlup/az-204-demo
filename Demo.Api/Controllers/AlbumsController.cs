using Demo.Api.Data;
using Demo.Api.Data.Entities;
using Demo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
