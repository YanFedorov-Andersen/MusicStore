using MusicStore.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class UserAccountRepository : IRepository<User>
    {
        private readonly MusicStoreContext _dataBase;

        public UserAccountRepository(MusicStoreContext dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(User item)
        {
            if (item != null)
            {
                _dataBase.Users.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            User user = _dataBase.Users.Find(id);

            if (user == null)
            {
                return -1;
            }

            _dataBase.Users.Remove(user);
            _dataBase.SaveChanges();
            return id;

        }

        public User GetItem(int id)
        {
            return _dataBase.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetItemList()
        {
            return _dataBase.Users.ToList();
        }

        public int Update(User item)
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
