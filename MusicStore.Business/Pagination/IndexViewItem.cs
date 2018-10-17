using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Business
{
    public class IndexViewItem<T>
    {
        public IEnumerable<T> Items { get; set; }
        public Pagination<T> PageInfo { get; set; }
    }
}
