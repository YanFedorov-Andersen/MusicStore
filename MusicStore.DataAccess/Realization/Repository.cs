﻿using MusicStore.DataAccess.Interfaces;
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

        private int pageNumber;
        private int pageSize;
        private int totalItems;
        public int PageNumber {
            set
            {
                if(value > 0)
                {
                    pageNumber = value;
                }
            }
            get
            {
                return pageNumber;
            }
        } 
        public int PageSize {
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                }
            }
            get
            {
                return pageSize;
            }
        } 
        public int TotalItems {
            set
            {
                if (value > 0)
                {
                    totalItems = value;
                }
            }
            get
            {
                return totalItems;
            }
        } 
        public int TotalPages 
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
            if (id < 1)
            {
                throw new ArgumentException("id < 1 in SongRepository");
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
            if (id < 1)
            {
                throw new ArgumentException("id less then 1", nameof(id));
            }

            try
            {
                return _dataContext.Set<T>().SingleOrDefault(x => x.Id == id);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("item in database");
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

        public IndexViewItem<T> MakePagination(List<T> items, int page = 1, int pageSize = 10)
        {
            IEnumerable<T> itemsPerPages = items.Skip((page - 1) * pageSize).Take(pageSize);
            PageNumber = page;
            PageSize = pageSize;
            TotalItems = items.Count;

            IndexViewItem<T> ivm = new IndexViewItem<T> { PageInfo = this, Items = itemsPerPages };
            return ivm;
        }
    }
}
