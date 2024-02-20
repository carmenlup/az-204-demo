using Demo.Api.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Duration { get; set; }
        public DateTime UploadTime { get; set; }
        public bool IsFeatured { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }       
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? AudioFile { get; set; }
        public string? AudioUrl { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; } = null!;
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
