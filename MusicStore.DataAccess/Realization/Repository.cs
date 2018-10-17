using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class Repository<T> : IRepository<T> 
        where T: Entity
    {
        private readonly MusicStoreContext _dataContext;
        public Repository(MusicStoreContext dataContext)
        {
            _dataContext = dataContext;
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
            try
            {
                return _dataContext.Set<T>().SingleOrDefault(x => x.Id == id);
            }
            catch (ArgumentNullException exception)
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
            throw new ArgumentException("item is null in SongRepository");
        }
    }
}
