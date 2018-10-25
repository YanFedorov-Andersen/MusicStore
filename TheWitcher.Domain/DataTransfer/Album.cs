using MusicStore.DataAccess;
using System.Collections.Generic;

namespace MusicStore.Domain.DataTransfer
{
    public class Album: Entity
    {
        public string Name { get; set; }
        public decimal DiscountIfBuyAllSongs { get; set; }
        public virtual ICollection<DataAccess.Song> Songs { get; set; }
    }
}
