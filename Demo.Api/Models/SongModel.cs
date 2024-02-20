using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Duration { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }

    }
}
