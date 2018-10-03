using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DataAccess
{
    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountIfBuyAllSongs { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
