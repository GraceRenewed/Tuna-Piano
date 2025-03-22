using Tuna_Piano.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace Tuna_Piano
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // allows passing datetimes without time zone data 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // allows our api endpoints to access the database through Entity Framework Core
            builder.Services.AddNpgsql<TunaPianoDbContext>(builder.Configuration["TunaPianoDbConnectionString"]);

            // Set the JSON serializer options
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapPost("/songs", (TunaPianoDbContext db, Song song) =>
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return Results.Created($"/songs/{song.Id}", song);
            });

            app.MapDelete("/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                Song song = db.Songs.SingleOrDefault(song => song.Id == id);
                if (song == null)
                {
                    return Results.NotFound();
                }
                db.Songs.Remove(song);
                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapPut("/songs/{id}", (TunaPianoDbContext db, int id, Song song) =>
            {
                Song songToUpdate = db.Songs.SingleOrDefault(song => song.Id == id);
                if (songToUpdate == null)
                {
                    return Results.NotFound();
                }
                songToUpdate.Title = song.Title;
                songToUpdate.ArtistId = song.ArtistId;
                songToUpdate.Album = song.Album;
                songToUpdate.Length = song.Length;
                songToUpdate.GenreId = song.GenreId;

                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapGet("/songs", (TunaPianoDbContext db) =>
            {
                return db.Songs.ToList();
            });

            app.MapGet("/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                return db.Songs
                    .Include(s => s.Artist)
                    .Include(s=> s.Genre)
                    .SingleOrDefault(s => s.Id == id);
            });

            app.MapPost("/artists", (TunaPianoDbContext db, Artist artist) =>
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return Results.Created($"/artists/{artist.Id}", artist);
            });

            app.MapDelete("/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                Artist artist = db.Artists.SingleOrDefault(artist => artist.Id == id);
                if (artist == null)
                {
                    return Results.NotFound();
                }
                db.Artists.Remove(artist);
                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapPut("/artists/{id}", (TunaPianoDbContext db, int id, Artist artist) =>
            {
                Artist artistToUpdate = db.Artists.SingleOrDefault(artist => artist.Id == id);
                if (artistToUpdate == null)
                {
                    return Results.NotFound();
                }
                artistToUpdate.Name = artist.Name;
                artistToUpdate.Age = artist.Age;
                artistToUpdate.Bio = artist.Bio;

                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapGet("/artists", (TunaPianoDbContext db) =>
            {
                return db.Artists.ToList();
            });

            app.MapGet("/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                return db.Artists
                    .Include(a => a.Songs)
                    .SingleOrDefault(a => a.Id == id);
            });

            app.MapPost("/genres", (TunaPianoDbContext db, Genre genre) =>
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return Results.Created($"/genres/{genre.Id}", genre);
            });

            app.MapDelete("/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                Genre genre = db.Genres.SingleOrDefault(genre => genre.Id == id);
                if (genre == null)
                {
                    return Results.NotFound();
                }
                db.Genres.Remove(genre);
                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapPut("/genres/{id}", (TunaPianoDbContext db, int id, Genre genre) =>
            {
                Genre genreToUpdate = db.Genres.SingleOrDefault(genre => genre.Id == id);
                if (genreToUpdate == null)
                {
                    return Results.NotFound();
                }
                genreToUpdate.Description = genre.Description;


                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapGet("/genres", (TunaPianoDbContext db) =>
            {
                return db.Genres.ToList();
            });

            app.MapGet("/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                return db.Genres
                    .Include(g => g.Songs)
                    .SingleOrDefault(g => g.Id == id);
            });

            app.Run();
        }
    }
}
