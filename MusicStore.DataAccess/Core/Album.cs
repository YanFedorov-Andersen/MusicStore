using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Album : Entity
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }
        public string Name { get; set; }
        public decimal DiscountIfBuyAllSongs { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
