using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DataAccess
{
    public class Artist
    {
        public Artist()
        {
            Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
