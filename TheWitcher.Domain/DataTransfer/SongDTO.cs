using MusicStore.DataAccess;

namespace MusicStore.Domain.DataTransfer
{
    public class Song: Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DataAccess.Album Album { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
    }
}
