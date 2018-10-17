using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Business.Interfaces
{
    public interface IPagination<T>
    {
        int PageNumber { get; set; } 
        int PageSize { get; set; }
        int TotalItems { get; set; } 
        int TotalPages { get; }
        IndexViewItem<T> MakePagination(List<T> items, int page = 1);
    }
}
