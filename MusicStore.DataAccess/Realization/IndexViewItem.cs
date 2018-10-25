using System.Collections.Generic;

namespace MusicStore.DataAccess.Realization
{
    public class IndexViewItem<T> where T : Entity
    {
        public IEnumerable<T> Items { get; set; }
        public Repository<T> PageInfo { get; set; }
    }
}
