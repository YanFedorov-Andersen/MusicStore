using MusicStore.DataAccess;
using System;

namespace MusicStore.Domain.DataTransfer
{
    public class BoughtSong: Entity
    {
        public decimal BoughtPrice { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool IsVisible { get; set; }
        public virtual DataAccess.Song Song { get; set; }
        public virtual DataAccess.User User { get; set; }
    }
}
