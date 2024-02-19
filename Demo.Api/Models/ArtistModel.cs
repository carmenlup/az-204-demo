using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Api.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl  { get; set; }
    }
}
