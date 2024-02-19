using Demo.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
    }
}
