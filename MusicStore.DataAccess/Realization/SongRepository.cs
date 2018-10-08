using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class SongRepository: IRepository<Song>, ISongStoreRepository
    {
        private readonly MusicStoreContext _dataBase;

        public SongRepository(MusicStoreContext dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(Song item)
        {
            if (item != null)
            {
                _dataBase.Songs.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            throw new ArgumentException("item is null in SongRepository");
        }

        public int Delete(int id)
        {
            if(id < 0)
            {
                throw new ArgumentException("id < 0 in SongRepository");
            }

            Song song = _dataBase.Songs.Find(id);

            if (song == null)
            {
                throw new ArgumentException("item is null in SongRepository");
            }

            _dataBase.Songs.Remove(song);
            _dataBase.SaveChanges();
            return id;
        }

        public Song GetItem(int id)
        {
            return _dataBase.Songs.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Song> GetItemList()
        {
            return _dataBase.Songs;
        }

        public int Update(Song item)
        {
            if (item != null)
            {
                _dataBase.Entry(item).State = EntityState.Modified;
                _dataBase.SaveChanges();
                return item.Id;
            }
            throw new ArgumentException("item is null in SongRepository");
        }

        public List<Song> GetAvailableSongsForBuyByUser(int userId)
        {
            if (userId >= 0)
            {
                var result = _dataBase.Database.SqlQuery<Song>("EXEC AvailableSongsForBuy @userId", new SqlParameter("@userId", userId));
                var songsList = result.ToListAsync().Result;
                return songsList;
            }
            throw new ArgumentException("id is null in SongRepository");
        }
    }
}
