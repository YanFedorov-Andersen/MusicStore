using MusicStore.DataAccess.Interfaces;
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
            return -1;
        }

        public int Delete(int id)
        {
            BoughtSong boughtSong = _dataBase.BoughtSongs.Find(id);

            if (boughtSong == null)
            {
                return -1;
            }

            _dataBase.BoughtSongs.Remove(boughtSong);
            _dataBase.SaveChanges();
            return id;
        }

        public BoughtSong GetItem(int id)
        {
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
            return -1;
        }
    }
}
