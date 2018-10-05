using MusicStore.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class SongRepository: IRepository<Song>
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
            return -1;
        }

        public int Delete(int id)
        {
            Song song = _dataBase.Songs.Find(id);

            if (song == null)
            {
                return -1;
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
            return -1;
        }

        public List<Song> GetAvailableSongsForBuyByUser(int userId)
        {
            if (userId >= 0)
            {
                var result = _dataBase.Database.SqlQuery<Song>("EXEC AvailableSongsForBuy @userId", new SqlParameter("@userId", userId));
                var songsList = result.ToListAsync().Result;
                return songsList;
            }
            return null;
        }
    }
}
