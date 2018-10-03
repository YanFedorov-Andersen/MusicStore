using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Address Address { get; set; }

    }
}
