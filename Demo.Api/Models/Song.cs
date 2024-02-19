using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Models
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
        public required string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? AudioFile { get; set; }
        public required string AudioUrl { get; set; }
        public int ArtistId { get; set; }
        public ArtistModel Artist { get; set; } = null!;
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }


    }
}
