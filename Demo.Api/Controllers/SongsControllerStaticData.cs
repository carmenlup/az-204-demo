using Demo.Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsControllerStaticData : ControllerBase
    {
        private static List<Song> _songs = new List<Song>
        {
            new Song { Id = 1, Title = "Song 1", Duration = "3.15", ImageUrl = "ImgUrl1", AudioUrl="AudioUrl1"},
            new Song { Id = 2, Title = "Song 2", Duration = "2.40", ImageUrl = "ImgUrl1", AudioUrl="AudioUrl2"}
        };

        [HttpGet]
        public IEnumerable<Song> Get()
        {
            return _songs;
        }

        [HttpPost]
        public void Post([FromBody] Song song)
        {
            _songs.Add(song);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Song song)
        {
            _songs[id-1] = song;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _songs.Remove(_songs[id-1]);
        }
    }
}
