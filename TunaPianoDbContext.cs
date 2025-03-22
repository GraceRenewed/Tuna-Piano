using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Tuna_Piano.Models;

namespace Tuna_Piano
{
    public class TunaPianoDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }

        public TunaPianoDbContext(DbContextOptions<TunaPianoDbContext> context) : base(context)
        { 
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
