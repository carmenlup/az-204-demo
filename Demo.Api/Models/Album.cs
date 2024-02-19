using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Models
{
    public class Album
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public int ArtistId { get; set; }

        [NotMapped]
        public required IFormFile Image { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();


    }
}
