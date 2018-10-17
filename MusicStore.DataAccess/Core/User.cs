using System;
using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class User
    {
        public User()
        {
            BoughtSongs = new HashSet<BoughtSong>();
        }

        public User(Guid identityKey)
        {
            IdentityKey = identityKey;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        public virtual ICollection<BoughtSong> BoughtSongs { get; set; }
        public virtual Address Address { get; set; }
        public Guid IdentityKey { get; set; }
        public bool IsActive { get; set; }
    }
}
