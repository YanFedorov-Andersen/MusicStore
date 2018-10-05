using MusicStore.DataAccess;
using System;

namespace TheWitcher.Domain.Models
{
    public class BoughtSongDTO
    {
        public int Id { get; set; }
        public decimal BoughtPrice { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool IsVisible { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
    }
}
