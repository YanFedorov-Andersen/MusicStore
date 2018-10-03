using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Album Album { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
        public virtual ICollection<BoughtSong> BoughtSongs { get; set; }
    }
}
