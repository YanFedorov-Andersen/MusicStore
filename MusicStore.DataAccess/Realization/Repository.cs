using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class Repository<T> : IGenericRepositoryWithPagination<T> 
        where T: Entity
    {
        private readonly MusicStoreContext _dataContext;

        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
        public Repository(MusicStoreContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Repository()
        {
        }

        public int Create(T item)
        {
            if (item != null)
            {
                _dataContext.Set<T>().Add(item);
                _dataContext.SaveChanges();
                return item.Id;
            }

            throw new ArgumentException("item is null in SongRepository");
        }

        public int Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id < 0 in SongRepository");
            }

            T item = _dataContext.Set<T>().Find(id);

            if (item == null)
            {
                throw new ArgumentException("item is null in SongRepository");
            }

            _dataContext.Set<T>().Remove(item);
            _dataContext.SaveChanges();
            return id;
        }

        public T GetItem(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id less then 0", nameof(id));
            }

            try
            {
                return _dataContext.Set<T>().SingleOrDefault(x => x.Id == id);
            }
            catch (Exception exception)
            {
                throw new ArgumentNullException("songs in database", exception.Message);
            }
        }

        public IEnumerable<T> GetItemList()
        {
            return _dataContext.Set<T>();
        }

        public int Update(T item)
        {
            if (item != null)
            {
                _dataContext.Entry(item).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return item.Id;
            }
            throw new ArgumentException("item is null in SongRepository", "item");
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
