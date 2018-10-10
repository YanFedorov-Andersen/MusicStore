using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class User
    {
        public User()
        {
            BoughtSongs = new HashSet<BoughtSong>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        public virtual ICollection<BoughtSong> BoughtSongs { get; set; }
        public virtual Address Address { get; set; }
        public string IdentityKey { get; set; }
    }
}
