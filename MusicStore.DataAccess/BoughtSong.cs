using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DataAccess
{
    public class BoughtSong
    {
        public int Id { get; set; }
        public decimal BoughtPrice { get; set; }
        public DateTime BoughtDate { get; set; }
        public bool IsVisible { get; set; }
        public virtual Song Song { get; set; }
        public virtual User User { get; set; }
    }
}
