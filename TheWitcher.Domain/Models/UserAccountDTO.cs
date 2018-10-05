using MusicStore.DataAccess;
using System.Collections.Generic;

namespace TheWitcher.Domain
{
    public class UserAccountDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        public ICollection<BoughtSong> BoughtSongs { get; set; }
    }
}
