using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class BoughtSongRepository: IRepository<BoughtSong>
    {
        private readonly MusicStoreContext _dataBase;

        public BoughtSongRepository(MusicStoreContext dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(BoughtSong item)
        {
            if (item != null)
            {
                _dataBase.BoughtSongs.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            throw new ArgumentException("item is null in BoughtSongRepository", nameof(item));
        }

        public int Delete(int id)
        {
            if(id < 1)
            {
                throw new ArgumentException("id less then 1", nameof(id));
            }

            BoughtSong boughtSong = _dataBase.BoughtSongs.Find(id);

            if (boughtSong == null)
            {
                throw new Exception("there is no available boughtsong with this id");
            }

            _dataBase.BoughtSongs.Remove(boughtSong);
            _dataBase.SaveChanges();
            return id;
        }

        public BoughtSong GetItem(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("id less then 1", nameof(id));
            }

            return _dataBase.BoughtSongs.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<BoughtSong> GetItemList()
        {
            return _dataBase.BoughtSongs.ToList();
        }

        public int Update(BoughtSong item)
        {
            if (item != null)
            {
                _dataBase.Entry(item).State = EntityState.Modified;
                _dataBase.SaveChanges();
                return item.Id;
            }
            throw new ArgumentException("item is null in BoughtSongRepository", nameof(item));
        }
    }
}
