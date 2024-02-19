using Demo.Api.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Data.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Gender { get; set; } // for bands
        public string? ImageUrl  { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        public ICollection<Album> Albums{ get; set; } = new List<Album>();
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }

}
