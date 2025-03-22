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
            modelBuilder.Entity<Artist>().HasData(new Artist[]
            {
                new Artist { Id = 101, Name = "Laura Story", Age = 45, Bio = "Laura Story is a contemporary Christian singer-songwriter and worship leader known for her Grammy-winning song 'Blessings' and her heartfelt lyrics about faith and perseverance." },
                new Artist { Id = 102, Name = "Chris Tomlin", Age = 51, Bio = "Chris Tomlin is an American contemporary Christian music artist, worship leader, and songwriter known for songs like 'How Great Is Our God'." },
                new Artist { Id = 103, Name = "Lauren Daigle", Age = 32, Bio = "Lauren Daigle is a contemporary Christian music singer and songwriter known for her soulful voice and hit songs like 'You Say'." },
                new Artist { Id = 104, Name = "TobyMac", Age = 59, Bio = "TobyMac is a Christian hip-hop and contemporary artist, formerly a member of DC Talk, known for his energetic and uplifting music." },
                new Artist { Id = 105, Name = "Elevation Worship", Age = 18, Bio = "Elevation Worship is a contemporary worship music band from Elevation Church, known for songs like 'Graves Into Gardens' and 'O Come to the Altar'." },
            });
       
            modelBuilder.Entity<Genre>().HasData(new Genre[] 
            {
                new Genre { Id = 201, Description = "Contemporary Christian" },
                new Genre { Id = 202, Description = "Worship" },
            });

            modelBuilder.Entity<Song>().HasData(new Song[]
            {
                new Song { Id = 301, Title = "How Great Is Our God", ArtistId = 1, Album = "Arriving", Length = 266 },
                new Song { Id = 302, Title = "You Say", ArtistId = 2, Album = "Look Up Child", Length = 274 },
                new Song { Id = 303, Title = "Speak Life", ArtistId = 3, Album = "Eye On It", Length = 205 },
                new Song { Id = 304, Title = "Graves Into Gardens", ArtistId = 4, Album = "Graves Into Gardens", Length = 452 },
                new Song { Id = 305, Title = "Indescribable", ArtistId = 1, Album = "Arriving", Length = 239 },
                new Song { Id = 306, Title = "Trust in You", ArtistId = 2, Album = "How Can It Be", Length = 237 },
                new Song { Id = 307, Title = "Blessings", ArtistId = 5, Album = "Blessings", Length = 295 },
                new Song { Id = 308, Title = "Mighty to Save", ArtistId = 101, Album = "Great God Who Saves", Length = 234}
            });

        }
    }
}
