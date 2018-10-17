using MusicStore.DataAccess;
using System.Collections.Generic;

namespace MusicStore.Domain.DataTransfer
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        public virtual Address Address { get; set; }
        public ICollection<DataAccess.BoughtSong> BoughtSongs { get; set; }
        public bool IsActive { get; set; }
    }
}
