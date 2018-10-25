using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Song : Entity
    {
        public Song()
        {
            BoughtSongs = new HashSet<BoughtSong>();
        }
        public Song(string name, decimal price, Album album = null, Genre genre = null, Artist artist = null)
        {
            Name = name;
            Price = price;
            Album = album;
            Genre = genre;
            Artist = artist;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<BoughtSong> BoughtSongs { get; set; }
    }
}
