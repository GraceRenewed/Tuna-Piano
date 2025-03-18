using System.ComponentModel.DataAnnotations;

namespace Tuna_Piano.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public int ArtistId {  get; set; }
        public Artist? Artist { get; set; }
        public string? Album {  get; set; }
        public int Length { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
