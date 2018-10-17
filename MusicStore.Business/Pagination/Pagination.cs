using MusicStore.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Business
{
    public class Pagination<T>: IPagination<T>
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public IndexViewItem<T> MakePagination(List<T> items, int page = 1)
        {
            int pageSize = 3; // количество объектов на страницу
            IEnumerable<T> itemsPerPages = items.Skip((page - 1) * pageSize).Take(pageSize);
            PageNumber = page;
            PageSize = pageSize;
            TotalItems = items.Count;

            IndexViewItem<T> ivm = new IndexViewItem<T> { PageInfo = this, Items = itemsPerPages };
            return ivm;
        }
    }
}
