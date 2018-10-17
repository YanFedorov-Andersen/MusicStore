using System;

namespace MusicStore.DataAccess
{
    public class BoughtSong : Entity
    {
        public decimal BoughtPrice { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool IsVisible { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
    }
}
