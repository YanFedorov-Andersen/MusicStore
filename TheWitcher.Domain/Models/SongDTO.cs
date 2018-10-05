using MusicStore.DataAccess;

namespace MusicStore.Domain.Models
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Album Album { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
    }
}
