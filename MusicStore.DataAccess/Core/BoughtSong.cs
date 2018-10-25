using System;

namespace MusicStore.DataAccess
{
    public class BoughtSong : Entity
    {
        public BoughtSong()
        {

        }
        public BoughtSong(decimal boughtPrice, DateTime boughtDate, bool isVisible)
        {
            BoughtPrice = boughtPrice;
            BoughtDate = boughtDate;
            IsVisible = isVisible;
        }
        public decimal BoughtPrice { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool IsVisible { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
    }
}
