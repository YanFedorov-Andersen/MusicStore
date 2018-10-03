using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Song
    {
        public Song()
        {
            BoughtSongs = new HashSet<BoughtSong>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<BoughtSong> BoughtSongs { get; set; }
    }
}
