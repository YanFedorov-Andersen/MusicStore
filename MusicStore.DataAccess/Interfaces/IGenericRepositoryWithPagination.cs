using MusicStore.DataAccess.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DataAccess.Interfaces
{
    public interface IGenericRepositoryWithPagination<T> where T: Entity
    {
        int PageNumber { get; set; } 
        int PageSize { get; set; } 
        int TotalItems { get; set; } 
        int TotalPages { get; } 
        IEnumerable<T> GetItemList();
        T GetItem(int id);
        int Create(T item);
        int Update(T item);
        int Delete(int id);
        IndexViewItem<T> MakePagination(List<T> items, int page = 1, int pageSize = 3);
    }
}
