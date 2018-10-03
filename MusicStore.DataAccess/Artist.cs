using System.Collections.Generic;

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
