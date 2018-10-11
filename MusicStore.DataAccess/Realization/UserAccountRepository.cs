using System;
using MusicStore.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.DataAccess.Realization
{
    public class UserAccountRepository : IRepository<User>, IUserAccountRepository
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
        public User GetItemWithGuidId(string id)
        {
            return _dataBase.Users.FirstOrDefault(x => x.IdentityKey == id);
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

        public bool CreateWithGuidId(string Id)
        {
            User user = new User()
            {
                IdentityKey = Id
            };
            _dataBase.Users.Add(user);
            _dataBase.SaveChanges();
            return true;
        }

        public int GetUserId(string identityId)
        {
            if (identityId == null)
            {
                throw new ArgumentException("Invalid user identity id");
            }

            var userId = _dataBase.Users.Where(x => x.IdentityKey == identityId).Select(x => x.Id);

            if (userId.Count() == 1)
            {
                return userId.FirstOrDefault();
            }

            throw new Exception("User can not found or found more then one");            
        }
    }
}
