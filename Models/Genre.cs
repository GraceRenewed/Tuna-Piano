namespace Tuna_Piano.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Description {  get; set; }
        public int SongId { get; set; }
        public Song? Song { get; set; }
        public List<Song> Songs { get; set; }
    }
}
